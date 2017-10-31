/* eslint no-param-reassign: 0 */

import axios from 'axios';
import { push } from 'react-router-redux';
import LocalStorage from '../localStorage';
import LocalStorageKeys from '../constants/localStorageKeys';
import ActionTypes from '../constants/actionTypes';
import Settings from '../settings';
import { RefreshToken } from '../api/';
import logger from '../logger';


const regex = /^.*\/api\/.*$/;

const authTokenInterceptor = (config) => {
  const r = regex.exec(config.url);
  if (r !== null) {
    // This is necessary to avoid infinite loops with zero-width matches
    const authData = LocalStorage.get(LocalStorageKeys.AUTHORIZATION_DATA);
    if (authData) {
      logger.debug(`add authorization header. url:${config.url}`);
      logger.debug(JSON.stringify(authData));
      config.headers.Authorization = `Bearer ${authData.access_token}`;
    }
  } else {
    logger.debug(`no auth request. url:${config.url}`);
  }
  return config;
}
;

let refreshTokenPromise = null;

const refreshTokenInterceptor = (store, error) => {
  if (error.response.status === 401) {
    const authData = LocalStorage.get(LocalStorageKeys.AUTHORIZATION_DATA);
    if (!refreshTokenPromise && authData && authData.refresh_token) {
      LocalStorage.remove(LocalStorageKeys.AUTHORIZATION_DATA);
      authData.appId = Settings.clientId;
      logger.debug('start token refresh');
      refreshTokenPromise = RefreshToken(authData)
        .then((response) => {
          store.dispatch({ type: ActionTypes.AUTHORIZATION_DATA, payload: response.data });
          refreshTokenPromise = null;
          // LocalStorage.set(LocalStorageKeys.AUTHORIZATION_DATA, response.data);
          logger.debug('token was refreshed');
          logger.debug(JSON.stringify(response.data));
        })
        .catch((err) => {
          logger.error(err);
          store.dispatch(push('/auth'));
          refreshTokenPromise = null;
          // it neen to stop executing in next 'then'
          throw err;
        });
    }


    if (refreshTokenPromise) {
      // wait refreshTokenPromise and repeat request
      logger.debug(`wait token refresh. url: ${error.config.url}`);
      return new Promise((resolve, reject) => {
        refreshTokenPromise
          .then(() => {
            logger.debug(`repeat ${error.config.url}`);
            const options = {
              method: error.config.method,
              url: error.config.url
            };
            if (error.config.data) {
              options.data = JSON.parse(error.config.data);
            }
            axios(options).then(resolve, reject);
          })
          .catch(reject);
      });
    }
  }
  return Promise.reject(error);
}
;

function addInterceptor(interceptor, isResponse, isErrorInterceptor) {
  const r = isResponse ? axios.interceptors.response : axios.interceptors.request;
  if (!r.handlers.find(h => h.fulfilled.name === interceptor.name)) {
    if (isResponse && isErrorInterceptor) {
      r.use(null, interceptor);
    } else {
      r.use(interceptor);
    }
  }
}

export default function add(store) {
  addInterceptor(authTokenInterceptor, false);
  addInterceptor(config => refreshTokenInterceptor(store, config), true, true);
}


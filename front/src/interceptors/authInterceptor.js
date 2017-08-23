/* eslint no-param-reassign: 0 */
import axios from 'axios';
import LocalStorage from '../localStorage';
import LocalStorageKeys from '../constants/localStorageKeys';
import ActionTypes from '../constants/actionTypes';
import Settings from '../settings';
import { RefreshToken } from '../api';

const regex = /^.*\/api\/.*$/g;

const authTokenInterceptor = (config) => {
  if ((regex.exec(config.url)) !== null) {
    // This is necessary to avoid infinite loops with zero-width matches
    const authData = LocalStorage.get('authorizationData');
    if (authData) {
      config.headers.Authorization = `Bearer ${authData.token}`;
    }
  }
  return config;
}
;

const refreshTokenInterceptor = (store, config) => {
  if (config.response.status === 401) {
    debugger;

    const authData = LocalStorage.get(LocalStorageKeys.AUTHORIZATION_DATA);

    if (authData) {
      if (authData.useRefreshTokens) {
        const data = `grant_type=refresh_token&refresh_token=${authData.refreshToken}&client_id=${Settings.clientId}`;
        LocalStorage.remove(LocalStorageKeys.AUTHORIZATION_DATA);
        RefreshToken(data).then((response) => {
          debugger;
          store.dispatch({ type: ActionTypes.AUTHORIZATION_DATA, payload: response.data });
        });
      }
    }
  }
  return config;
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
  addInterceptor((config) => { refreshTokenInterceptor(store, config); }, true, true);
}

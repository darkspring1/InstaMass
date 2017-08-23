/* eslint max-len: 0 */
import axios from 'axios';
import Settings from './settings';

function RegisterExternal(registerExternalData) {
  return axios.post(`${Settings.ApiServiceBaseUri}api/account/registerexternal`, registerExternalData);
}

function ObtainLocalAccessToken(externalData) {
  const url = `${Settings.ApiServiceBaseUri}api/account/ObtainLocalAccessToken`;
  return axios.get(url, { params: externalData });
}

function Orders(externalData) {
  const url = `${Settings.ApiServiceBaseUri}api/orders`;
  return axios.get(url, { params: externalData });
}
/*
const _refreshToken = function () {
  const deferred = $q.defer();

  const authData = localStorageService.get('authorizationData');

  if (authData) {
    if (authData.useRefreshTokens) {
      const data = `grant_type=refresh_token&refresh_token=${authData.refreshToken}&client_id=${ngAuthSettings.clientId}`;

      localStorageService.remove('authorizationData');

      $http.post(`${serviceBase}token`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success((response) => {
        localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

        deferred.resolve(response);
      }).error((err, status) => {
        _logOut();
        deferred.reject(err);
      });
    }
  }

  return deferred.promise;
}; */

export { RegisterExternal, ObtainLocalAccessToken, Orders };

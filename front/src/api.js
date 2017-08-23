/* eslint max-len: 0 */
import axios from 'axios';
import Settings from './settings';

function RegisterExternal(registerExternalData) {
  return axios.post(`${Settings.apiServiceBaseUri}api/account/registerexternal`, registerExternalData);
}

function ObtainLocalAccessToken(externalData) {
  const url = `${Settings.apiServiceBaseUri}api/account/ObtainLocalAccessToken`;
  return axios.get(url, { params: externalData });
}

function Orders() {
  const url = `${Settings.apiServiceBaseUri}api/orders`;
  return axios.get(url);
}

function RefreshToken(data) {
  return axios.post(`${Settings.apiServiceBaseUri}token`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}

export { RegisterExternal, ObtainLocalAccessToken, Orders, RefreshToken };

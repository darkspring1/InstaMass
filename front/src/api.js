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

export { RegisterExternal, ObtainLocalAccessToken, Orders };

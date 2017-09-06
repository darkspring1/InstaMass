/* eslint max-len: 0 */
import axios from 'axios';
import Settings from './settings';

/*
function AddOrGetExisting(key, promiseFunc) {
  let result = AddOrGetExisting[key];
  if (result) {
    result = promiseFunc().finally(() => {
      delete AddOrGetExisting[key];
    });
    AddOrGetExisting[key] = result;
  }
  return result;
}
*/

function RegisterExternal(registerExternalData) {
  return axios.post(`${Settings.apiServiceBaseUri}api/user/registerexternal`, registerExternalData);
}

function ObtainLocalAccessToken(externalData) {
  const url = `${Settings.apiServiceBaseUri}api/user/ObtainLocalAccessToken`;
  return axios.get(url, { params: externalData });
}

function Orders() {
  const url = `${Settings.apiServiceBaseUri}api/orders`;
  return axios.get(url);
}

function RefreshToken(externalData) {
  const data = `grant_type=refresh_token&refresh_token=${externalData.refresh_token}&client_id=${externalData.appId}`;
  return axios.post(`${Settings.apiServiceBaseUri}token`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}

function Login(email, password, appId) {
  debugger;
  let data = `grant_type=password&username=${email}&password=${password}`;
  data = `${data}&client_id=${appId}`;

  return axios.post(`${Settings.apiServiceBaseUri}token`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}
/*
function LoginExternal(externalData) {
  return axios.post(`${Settings.apiServiceBaseUri}token`, externalData, { headers: { 'grant-type': 'external-token' } });
} */

function LoginExternal(externalData) {
  let data = `grant_type=password&external_token=${externalData.externalAccessToken}&provider=${externalData.provider}`;
  data = `${data}&client_id=${externalData.appId}`;

  return axios.post(`${Settings.apiServiceBaseUri}token`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}


function AddNewAccount(newAccount) {
  return axios.post(`${Settings.apiServiceBaseUri}api/account`, newAccount);
}

export { RegisterExternal, ObtainLocalAccessToken, Orders, RefreshToken, Login, LoginExternal, AddNewAccount };

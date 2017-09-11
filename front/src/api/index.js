/* eslint max-len: 0 */
import axios from 'axios';
import Settings from '../settings';

function responseWrap(apiPromise) {
  return apiPromise.then(response => response.data)
  .catch((e) => {
    const r = e.response;
    const err = {
      status: r.status,
      statusText: r.statusText
    };
    Object.assign(err, r.data);
    throw err;
  });
}

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
  return responseWrap(axios.post(`${Settings.apiServiceBaseUri}api/account`, newAccount));
}

function GetAccounts() {
  return responseWrap(axios.get(`${Settings.apiServiceBaseUri}api/accounts`));
}

export { GetAccounts, RegisterExternal, ObtainLocalAccessToken, Orders, RefreshToken, Login, LoginExternal, AddNewAccount };

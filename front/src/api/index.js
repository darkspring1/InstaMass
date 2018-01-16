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
  return axios.post(`${Settings.apiServiceBaseUri}user/registerexternal`, registerExternalData);
}

function ObtainLocalAccessToken(externalData) {
  const url = `${Settings.apiServiceBaseUri}user/ObtainLocalAccessToken`;
  return axios.get(url, { params: externalData });
}

function Orders() {
  const url = `${Settings.apiServiceBaseUri}orders`;
  return axios.get(url);
}

function RefreshToken(externalData) {
  const data = `grant_type=refresh_token&refresh_token=${externalData.refresh_token}&client_id=${externalData.appId}`;
  return axios.post(`${Settings.apiServiceBaseUri}token`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}

function Login(email, password) {
  debugger;
  const data = `email=${email}&password=${password}`;
  return axios.post(`${Settings.apiServiceBaseUri}user/login`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}

function Register(email, userName, password) {
  debugger;
  const data = `email=${email}&username=${userName}&password=${password}`;
  return axios.post(`${Settings.apiServiceBaseUri}user/login`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}


function LoginExternal(externalData) {
  debugger;
  let data = `grant_type=password&external_token=${externalData.externalAccessToken}&provider=${externalData.provider}`;
  data = `${data}&client_id=${externalData.appId}`;
  return axios.post(`${Settings.apiServiceBaseUri}user/loginExternal`, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
}


function AddNewAccount(newAccount) {
  return responseWrap(axios.post(`${Settings.apiServiceBaseUri}account`, newAccount));
}

function GetAccounts() {
  return responseWrap(axios.get(`${Settings.apiServiceBaseUri}accounts`));
}

function CreateTagTask(task) {
  return responseWrap(axios.post(`${Settings.apiServiceBaseUri}task/tag`, task));
}


export { CreateTagTask, GetAccounts, RegisterExternal, Register, ObtainLocalAccessToken, Orders, RefreshToken, Login, LoginExternal, AddNewAccount };

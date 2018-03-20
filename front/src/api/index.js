/* eslint max-len: 0 */
import axios from 'axios';
import Settings from '../settings';

function post(params) {
  const absolutUrl = params.absolutUrl ? params.absolutUrl : `${Settings.apiServiceBaseUri}${params.url}`;
  const headers = params.headers || {};
  const data = params.data || {};
  if (!headers['Content-Type']) {
    headers['Content-Type'] = 'application/json';
  }

  return axios.post(absolutUrl, data, { headers });
}

function responseWrap(apiPromise) {
  return apiPromise.then(response => response.data)
  .catch((e) => {
    const res = e.response;
    if (res) {
      const err = {
        status: res.status,
        statusText: res.statusText
      };
      Object.assign(err, res.data);
      throw err;
    }
    throw e;
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
  const headers = { Authorization: `Bearer ${externalData.refreshToken}` };
  return post({ url: 'user/token/refresh', headers });
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

function GetTasks() {
  return responseWrap(axios.get(`${Settings.apiServiceBaseUri}tasks`));
}

function CreateTagTask(task) {
  return responseWrap(axios.post(`${Settings.apiServiceBaseUri}tasks/tag`, task));
}

function GetTagTask(id) {
  return responseWrap(axios.get(`${Settings.apiServiceBaseUri}tasks/tag/${id}`));
}

function UpdateTagTask(taskId, task) {
  return responseWrap(axios.put(`${Settings.apiServiceBaseUri}tasks/tag/${taskId}`, task));
}


export { CreateTagTask, GetAccounts, GetTasks,
  RegisterExternal, Register, ObtainLocalAccessToken,
  Orders, RefreshToken, Login, LoginExternal,
  AddNewAccount, GetTagTask, UpdateTagTask };

import ActionTypes from '../constants/actionTypes';

import AuthExternalProvider from './authExternalProvider';
import Login from './login';

export { AuthExternalProvider };
export { Login };

function ActionConstructor(type, payloadModifier) {
  let action;
  if (payloadModifier) {
    action = payload => ({ type, payload: payloadModifier(payload) });
  } else {
    action = payload => ({ type, payload });
  }
  action.type = type;
  return action;
}


export function AddNewAccountSucceeded(payload) {
  return { type: ActionTypes.ADD_NEW_ACCOUNT_SUCCEEDED, payload };
}

export function AddNewAccountFailed(payload) {
  return { type: ActionTypes.ADD_NEW_ACCOUNT_FAILED, payload };
}


export function GetAccountsData(payload) {
  return { type: ActionTypes.GET_ACCOUNTS_DATA, payload };
}

export function AddNewAccountRequested(payload) {
  return { type: ActionTypes.ADD_NEW_ACCOUNT_REQUESTED, payload };
}

export function AccountsRequested(payload) {
  return { type: ActionTypes.ACCOUNTS_REQUESTED, payload };
}

export function AccountsLoaded(payload) {
  return { type: ActionTypes.ACCOUNTS_LOADED, payload };
}


export function AddNewTagRequested(payload) {
  return { type: ActionTypes.ADD_NEW_TAG_REQUESTED, payload };
}

export function SignIn(payload) {
  return { type: ActionTypes.SIGN_IN_REQUESTED, payload };
}

export function SignInExternal(payload) {
  return { type: ActionTypes.SIGN_IN_EXTERNAL_REQUESTED, payload };
}

export function SignUp(payload) {
  return { type: ActionTypes.SIGN_UP_REQUESTED, payload };
}

export function SignUpExternal(payload) {
  return { type: ActionTypes.SIGN_UP_EXTERNAL_REQUESTED, payload };
}

export function AuthorizationData(payload) {
  return { type: ActionTypes.AUTHORIZATION_DATA, payload };
}

const LOCATION_CHANGE = '@@router/LOCATION_CHANGE';

const RequestError = ActionConstructor('REQUEST_ERROR');

const RequestStarted = ActionConstructor('REQUEST_STARTED', (payload) => {
  const p = payload || {};
  p.preloader = p.preloader || 'top';
  return p;
});

const RequestFinished = ActionConstructor('REQUEST_FINISHED', (payload) => {
  const p = payload || {};
  p.preloader = p.preloader || 'top';
  return p;
});

const ShowToastr = ActionConstructor('SHOW_TOASTR');

// загрузка списка задач
const TasksRequested = ActionConstructor('TASKS_REQUESTED');
const TasksLoaded = ActionConstructor('TASKS_LOADED');

// запрос на загрузка с сервера Tag task
const TagTaskGetRequest = ActionConstructor('TAG_TASK_REQUEST');

// создание или обновление TagTask сторе
const TagTaskCreateOrUpdate = ActionConstructor('TAG_TASK_CREAT_OR_UPDATE');

// запрос на создание или обновление на сервере
const TagTaskUpdateRequest = ActionConstructor('TAG_TASK_UPDATE_REQUEST');
const TagTaskCreateRequest = ActionConstructor('TAG_TASK_CREATE_REQUEST');


const TaskDeleteRequest = ActionConstructor('TASK_DELETE_REQUEST');

export {
  LOCATION_CHANGE,
  RequestError,
  RequestStarted,
  RequestFinished,
  ShowToastr,
  TasksRequested,
  TasksLoaded,
  TagTaskGetRequest,
  TagTaskCreateOrUpdate,
  TagTaskCreateRequest,
  TagTaskUpdateRequest,
  TaskDeleteRequest
};

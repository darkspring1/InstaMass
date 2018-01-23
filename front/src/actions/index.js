import ActionTypes from '../constants/actionTypes';

import AuthExternalProvider from './authExternalProvider';
import Login from './login';

export { AuthExternalProvider };
export { Login };

export function RequestStarted(payload) {
  const p = payload || {};
  p.preloader = p.preloader || 'top';
  return { type: ActionTypes.REQUEST_STARTED, payload: p };
}

export function RequestFinished(payload) {
  const p = payload || {};
  p.preloader = p.preloader || 'top';
  return { type: ActionTypes.REQUEST_FINISHED, payload: p };
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

export function ShowToastr(payload) {
  return { type: ActionTypes.SHOW_TOASTR, payload };
}

export function RequestError(payload) {
  return { type: ActionTypes.REQUEST_ERROR, payload };
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

export function AddNewTagSucceeded(payload) {
  return { type: ActionTypes.ADD_NEW_TAG_SUCCEEDED, payload };
}

export function AddNewTagTaskRequested(payload) {
  return { type: ActionTypes.ADD_NEW_TAG_TASK_REQUESTED, payload };
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

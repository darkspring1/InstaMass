import ActionTypes from '../constants/actionTypes';

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

export function AddNewLikeTaskRequested(payload) {
  return { type: ActionTypes.ADD_NEW_LIKE_TASK_REQUESTED, payload };
}

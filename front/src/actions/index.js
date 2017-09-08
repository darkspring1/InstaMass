import ActionTypes from '../constants/actionTypes';

export function RequestStarted(payload) {
  return { type: ActionTypes.REQUEST_STARTED, payload };
}

export function RequestFinished(payload) {
  return { type: ActionTypes.REQUEST_FINISHED, payload };
}

export function AddNewAccountSucceeded(payload) {
  return { type: ActionTypes.ADD_NEW_ACCOUNT_SUCCEEDED, payload };
}

export function AddNewAccountFailed(payload) {
  return { type: ActionTypes.ADD_NEW_ACCOUNT_FAILED, payload };
}

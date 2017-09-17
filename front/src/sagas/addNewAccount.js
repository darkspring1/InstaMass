import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from '../constants/actionTypes';

import * as Actions from '../actions';
import { AddNewAccount } from '../api/';

function* fetch(action) {
  try {
    yield put(Actions.RequestStarted());
    const newAccount = yield AddNewAccount(action.payload);
    yield put(Actions.AddNewAccountSucceeded(newAccount));
    yield put(Actions.ShowToastr());
  } catch (e) {
    yield put(Actions.AddNewAccountFailed(e));
  }
  yield put(Actions.RequestFinished());
}

export default function* AddNewAccountSaga() {
  yield takeEvery(ActionTypes.ADD_NEW_ACCOUNT_REQUESTED, fetch);
}

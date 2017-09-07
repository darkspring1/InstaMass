import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from '../constants/actionTypes';
import { AddNewAccount } from './../api';

function* fetch(action) {
  try {
    const user = yield AddNewAccount(action.payload);
    // debugger;
    yield put({ type: ActionTypes.ADD_NEW_ACCOUNT_SUCCEEDED, payload: user });
  } catch (e) {
    yield put({ type: ActionTypes.ADD_NEW_ACCOUNT_FAILED, payload: e.message });
  }
}

export default function* AddNewAccountSaga() {
  yield takeEvery(ActionTypes.ADD_NEW_ACCOUNT_REQUESTED, fetch);
}

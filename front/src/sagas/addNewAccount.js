import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from '../constants/actionTypes';

import * as Actions from '../actions';
import { AddNewAccount } from './../api';

function* fetch(action) {
  try {
    yield put(Actions.RequestStarted('top'));
    const newAccount = yield AddNewAccount(action.payload);
    yield put(Actions.AddNewAccountSucceeded(newAccount));
  } catch (e) {
    yield put(Actions.AddNewAccountFailed(e));
  }
  yield put(Actions.RequestFinished('top'));
}

export default function* AddNewAccountSaga() {
  yield takeEvery(ActionTypes.ADD_NEW_ACCOUNT_REQUESTED, fetch);
}

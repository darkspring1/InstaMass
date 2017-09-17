import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from '../constants/actionTypes';

import * as Actions from '../actions';
// import { AddNewAccount } from '../api/';

function* fetch(action) {
  try {
    yield put(Actions.RequestStarted());
    // const newAccount = yield AddNewAccount(action.payload);
    yield put(Actions.AddNewTagSucceeded({ tag: action.payload, total: '100500' }));
    // yield put(Actions.ShowToastr());
  } catch (e) {
    debugger;
    yield put(Actions.AddNewTagFailed(e));
  }
  yield put(Actions.RequestFinished());
}

export default function* AddNewTagSaga() {
  yield takeEvery(ActionTypes.ADD_NEW_TAG_REQUESTED, fetch);
}

import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from '../constants/actionTypes';

import * as Actions from '../actions';
import { CreateTagTask } from '../api/';

function* fetch(action) {
  try {
    yield put(Actions.RequestStarted());
    debugger;
    yield CreateTagTask(action.payload);
    yield put(Actions.ShowToastr());
  } catch (e) {
    debugger;
    // yield put(Actions.AddNewTagFailed(e));
  }
  yield put(Actions.RequestFinished());
}

export default function* AddNewLikeTaskSaga() {
  yield takeEvery(ActionTypes.ADD_NEW_TAG_TASK_REQUESTED, fetch);
}

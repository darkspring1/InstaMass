import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from '../constants/actionTypes';

import * as Actions from '../actions';
import { CreateLikeTask } from '../api/';

function* fetch(action) {
  try {
    yield put(Actions.RequestStarted());
    yield CreateLikeTask(action.payload);
    yield put(Actions.ShowToastr());
  } catch (e) {
    debugger;
    // yield put(Actions.AddNewTagFailed(e));
  }
  yield put(Actions.RequestFinished());
}

export default function* AddNewLikeTaskSaga() {
  yield takeEvery(ActionTypes.ADD_NEW_LIKE_TASK_REQUESTED, fetch);
}

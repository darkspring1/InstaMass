import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from 'constants/actionTypes';
import { GetTasks } from 'api';
import { TasksLoaded, RequestStarted, RequestFinished, RequestError } from 'actions';


function* fetch(/* action */) {
  try {
    yield put(RequestStarted());
    const tasks = yield GetTasks();
    yield put(TasksLoaded(tasks));
  } catch (e) {
    yield put(RequestError(e));
  }
  yield put(RequestFinished());
}

export default function* GetTasksSaga() {
  yield takeEvery(ActionTypes.TASKS_REQUESTED, fetch);
}

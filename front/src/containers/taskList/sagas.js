import { put, /* select, */takeEvery } from 'redux-saga/effects';

import { GetTasks } from 'api';
import { TasksRequested, TasksLoaded, RequestStarted, RequestFinished, RequestError } from 'actions';


function* fetch(/* action */) {
  try {
    yield put(RequestStarted.create());
    const tasks = yield GetTasks();
    yield put(TasksLoaded.create(tasks));
  } catch (e) {
    yield put(RequestError.create(e));
  }
  yield put(RequestFinished.create());
}

export default function* GetTasksSaga() {
  yield takeEvery(TasksRequested.type, fetch);
}

import { put, /* select, */takeEvery } from 'redux-saga/effects';


import { TagTaskRequested, TagTaskLoaded, RequestStarted, RequestFinished, RequestError } from 'actions';

import { GetTagTask } from 'api';

function* fetch(action) {
  debugger;
  try {
    yield put(RequestStarted.create());
    const task = yield GetTagTask(action.payload.id);
    debugger;
    yield put(TagTaskLoaded.create(task));
  } catch (e) {
    yield put(RequestError.create(e));
  }
  yield put(RequestFinished.create());
}

export default function* GetTagTaskSaga() {
  yield takeEvery(TagTaskRequested.type, fetch);
}

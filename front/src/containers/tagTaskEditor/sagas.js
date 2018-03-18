import { put, /* select, */takeEvery } from 'redux-saga/effects';


import { TagTaskRequested, TagTaskLoaded, RequestStarted, RequestFinished, RequestError } from 'actions';

import { GetTagTask } from 'api';

function* fetch(action) {
  try {
    yield put(RequestStarted());
    const task = yield GetTagTask(action.payload.id);
    yield put(TagTaskLoaded(task));
  } catch (e) {
    yield put(RequestError(e));
  }
  yield put(RequestFinished());
}

export default function* GetTagTaskSaga() {
  yield takeEvery(TagTaskRequested.type, fetch);
}

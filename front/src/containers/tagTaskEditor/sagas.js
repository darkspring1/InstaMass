import { put, /* select, */takeEvery } from 'redux-saga/effects';
import push from 'utils/';
import * as Routes from 'constants/routes';

import { TagTaskGetRequest, ShowToastr,
  RequestStarted,
  RequestFinished, RequestError, TagTaskCreateOrUpdate,
  TagTaskCreateRequest, TagTaskUpdateRequest
} from 'actions';

import { GetTagTask, CreateTagTask, UpdateTagTask } from 'api';

function* run(func) {
  try {
    yield func();
  } catch (e) {
    yield put(RequestError(e));
  }
  yield put(RequestFinished());
}

function* fetchGetTagTask(action) {
  function* r() {
    yield put(RequestStarted());
    const task = yield GetTagTask(action.payload.id);
    yield put(TagTaskCreateOrUpdate(task));
  }
  yield run(r);
}

function* fetchCreateTagTask(action) {
  function* r() {
    yield put(RequestStarted());
    const task = yield CreateTagTask(action.payload);
    yield put(push(Routes.TAG_TASK_EDITOR, { id: task.id }));
    // yield put(TagTaskCreateOrUpdate(task));
    // yield put(ShowToastr());
  }
  yield run(r);
}

function* fetchUpdateTagTask(action) {
  function* r() {
    debugger;
    yield put(RequestStarted());
    yield UpdateTagTask(action.payload.taskId, action.payload.task);
    yield put(TagTaskCreateOrUpdate());
    yield put(ShowToastr());
  }
  yield run(r);
}

export function* CreateTagTaskSaga() {
  yield takeEvery(TagTaskCreateRequest.type, fetchCreateTagTask);
}

export function* UpdateTagTaskSaga() {
  yield takeEvery(TagTaskUpdateRequest.type, fetchUpdateTagTask);
}

export function* GetTagTaskSaga() {
  yield takeEvery(TagTaskGetRequest.type, fetchGetTagTask);
}

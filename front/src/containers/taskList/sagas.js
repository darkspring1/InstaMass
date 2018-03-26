import { put, /* select, */takeEvery } from 'redux-saga/effects';

import { GetTasks, DeleteTask } from 'api';
import { TasksRequested, TasksLoaded,
  RequestStarted,
  RequestFinished, RequestError,
  TaskDeleteRequest, TaskDelete,
  ShowToastr } from 'actions';


function* getTasksFetch(/* action */) {
  try {
    yield put(RequestStarted());
    const tasks = yield GetTasks();
    yield put(TasksLoaded(tasks));
  } catch (e) {
    yield put(RequestError(e));
  }
  yield put(RequestFinished());
}

function* deleteTaskFetch(action) {
  try {
    const taskId = action.payload;
    yield put(RequestStarted());
    yield DeleteTask(taskId);
    yield put(TaskDelete(taskId));
    yield put(ShowToastr('Задача удалена'));
  } catch (e) {
    yield put(RequestError(e));
  }
  yield put(RequestFinished());
}

export function* GetTasksSaga() {
  yield takeEvery(TasksRequested.type, getTasksFetch);
}

export function* DeleteTaskSaga() {
  yield takeEvery(TaskDeleteRequest.type, deleteTaskFetch);
}

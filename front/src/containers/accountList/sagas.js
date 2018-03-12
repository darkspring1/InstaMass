import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from 'constants/actionTypes';

import * as Actions from 'actions';
import { GetAccounts } from 'api';

function* fetch(/* action */) {
  try {
    yield put(Actions.RequestStarted());
    const accounts = yield GetAccounts();
    yield put(Actions.AccountsLoaded(accounts));
    yield put(Actions.ShowToastr({ message: 'Аккаунт был успешно создан' }));
  } catch (e) {
    yield put(Actions.AccountsLoaded());
    yield put(Actions.RequestError(e));
  }
  yield put(Actions.RequestFinished());
}

export default function* GetAccountsSaga() {
  yield takeEvery(ActionTypes.ACCOUNTS_REQUESTED, fetch);
}

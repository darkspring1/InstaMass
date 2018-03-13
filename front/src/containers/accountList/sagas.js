import { put, /* select, */takeEvery } from 'redux-saga/effects';

import ActionTypes from 'constants/actionTypes';

import * as Actions from 'actions';
import { GetAccounts } from 'api';

function* fetch(/* action */) {
  try {
    yield put(Actions.RequestStarted.create());
    const accounts = yield GetAccounts();
    yield put(Actions.AccountsLoaded(accounts));
    yield put(Actions.ShowToastr.create({ message: 'Аккаунт был успешно создан' }));
  } catch (e) {
    yield put(Actions.AccountsLoaded());
    yield put(Actions.RequestError.create(e));
  }
  yield put(Actions.RequestFinished.create());
}

export default function* GetAccountsSaga() {
  yield takeEvery(ActionTypes.ACCOUNTS_REQUESTED, fetch);
}

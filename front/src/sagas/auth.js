/* eslint no-unused-vars: 0 */

import { put, /* select, */takeEvery } from 'redux-saga/effects';
import { push } from 'react-router-redux';
import ActionTypes from 'constants/actionTypes';
import AuthSettings from 'settings';
import { AddNewAccount } from 'api/';

import * as Actions from 'actions';


function openAuthWindow(provider) {
  const promise = new Promise((resolve /* , reject */) => {
    window.dispatchAuthExternalProvider = (authInfo) => {
      resolve(authInfo);
    };
  });

/* не переводить строку */
  const externalProviderUrl = `${AuthSettings.apiServiceBaseUri}user/LoginExternal?provider=${provider}`;
  window.open(externalProviderUrl, 'Authenticate Account', 'location=0,status=0,width=600,height=750');
  return promise;
}

function fetch(action) {
  console.log('ffff');
}

function* fetchSignInExternal(action) {
  try {
    const authInfo = yield openAuthWindow(action.payload);
    yield put(Actions.AuthorizationData(authInfo));
    yield put(push('/dashboard'));
  } catch (e) {
    debugger;
    // yield put(Actions.AddNewAccountFailed(e));
  }
  // yield put(Actions.RequestFinished());
}

export function* SignIn() {
  yield takeEvery(ActionTypes.SIGN_IN_REQUESTED, fetch);
}

export function* SignInExternal() {
  yield takeEvery(ActionTypes.SIGN_IN_EXTERNAL_REQUESTED, fetchSignInExternal);
}

export function* SignUp() {
  yield takeEvery(ActionTypes.SIGN_UP_REQUESTED, fetch);
}

export function* SignUpExternal() {
  yield takeEvery(ActionTypes.SIGN_UP_EXTERNAL_REQUESTED, fetch);
}


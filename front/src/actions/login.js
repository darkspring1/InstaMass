/* eslint max-len: 0  */
/* eslint no-unused-vars: 0  */
import { push } from 'react-router-redux';
import Settings from './../settings';
import ActionTypes from './../constants/actionTypes';
import LocalStorageKeys from './../constants/localStorageKeys';
import { Login } from './../api';
import LocalStorage from '../localStorage';

function action(loginData, dispatch) {
  debugger;
  return Login(loginData.email, loginData.password, Settings.clientId)
  .then((response) => {
    debugger;
  });
}

export default loginData => dispatch => action(loginData, dispatch);

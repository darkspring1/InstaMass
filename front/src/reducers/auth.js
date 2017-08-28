
import ActionTypes from './../constants/actionTypes';
import LocalStorageKeys from '../constants/localStorageKeys';
import LocalStorage from '../localStorage';

export default function auth(state = {}, action) {
  if (action.type === ActionTypes.AUTHORIZATION_DATA) {
    LocalStorage.set(LocalStorageKeys.AUTHORIZATION_DATA, action.payload);
    return {
      ...state, isAuth: true
    };
  }
  return state;
}

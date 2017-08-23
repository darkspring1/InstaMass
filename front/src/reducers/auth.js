
import ActionTypes from './../constants/actionTypes';
import LocalStorageKeys from '../constants/localStorageKeys';
import LocalStorage from '../localStorage';

export default function playlist(state = {}, action) {
  if (action.type === ActionTypes.AUTHORIZATION_DATA) {
    LocalStorage.set(LocalStorageKeys.AUTHORIZATION_DATA, { token: action.payload.access_token,
      userName: action.payload.userName,
      refreshToken: '',
      useRefreshTokens: true });
    return {
      ...state, isAuth: true
    };
  }
  return state;
}

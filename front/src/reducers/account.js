
import ActionTypes from './../constants/actionTypes';

export default function account(state = {}, action) {
  if (action.type === ActionTypes.ADD_NEW_ACCOUNT_FAILED) {
    return {
      ...state, requestError: action.payload
    };
  } else if (action.type === ActionTypes.ACCOUNTS_LOADED) {
    return {
      ...state, accounts: { state: 'loaded', list: action.payload }
    };
  } else if (action.type === ActionTypes.ACCOUNTS_LOADING) {
    const accounts = { state: 'loading' };
    return {
      ...state, accounts
    };
  }
  return state;
}

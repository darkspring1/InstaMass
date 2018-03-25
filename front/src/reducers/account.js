
import { LOCATION_CHANGE } from 'actions';
import ActionTypes from './../constants/actionTypes';

const defaultState = null;

export default function account(state = defaultState, action) {
  if (action.type === ActionTypes.ADD_NEW_ACCOUNT_FAILED) {
    return {
      ...state, requestError: action.payload
    };
  } else if (action.type === ActionTypes.ACCOUNTS_LOADED) {
    return action.payload;
  } else if (action.type === LOCATION_CHANGE) {
    return defaultState;
  }

  return state;
}

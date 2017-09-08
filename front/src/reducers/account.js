
import ActionTypes from './../constants/actionTypes';

export default function account(state = {}, action) {
  if (action.type === ActionTypes.ADD_NEW_ACCOUNT_FAILED) {
    return {
      ...state, requestError: action.payload
    };
  }
  return state;
}

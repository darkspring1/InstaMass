
import ActionTypes from './../constants/actionTypes';


export default function playlist(state = {}, action) {
  if (action.type === ActionTypes.AUTHORIZATION_DATA) {
    return {
      ...state,
      ...action.payload
    };
  }
  return state;
}

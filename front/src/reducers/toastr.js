
import ActionTypes from './../constants/actionTypes';

export default function toastr(state = {}, action) {
  if (action.type === ActionTypes.SHOW_TOASTR) {
    const result = { ...state, message: action.payload.message };
    return result;
  }
  return state;
}

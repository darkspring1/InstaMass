import { ShowToastr } from 'actions';

function GetMessage(payload) {
  if (typeof (payload) === 'string') {
    return payload;
  }

  return payload.message;
}

export default function toastr(state = {}, action) {
  if (action.type === ShowToastr.type) {
    const message = GetMessage(action.payload);
    const result = { ...state, message };
    return result;
  }
  return state;
}


import ActionTypes from 'constants/actionTypes';

export default function account(state = [], action) {
  if (action.type === ActionTypes.TASKS_LOADED) {
    return action.payload;
  }

  return state;
}

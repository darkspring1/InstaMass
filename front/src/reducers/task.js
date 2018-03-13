
import { LOCATION_CHANGE, TasksLoaded } from 'actions';

const defaultState = [];

export default function account(state = defaultState, action) {
  if (action.type === TasksLoaded.type) {
    return action.payload;
  }

  if (action.type === LOCATION_CHANGE) {
    return defaultState;
  }

  return state;
}

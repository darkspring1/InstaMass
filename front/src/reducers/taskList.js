
import { LOCATION_CHANGE, TasksLoaded, TaskDelete } from 'actions';

const defaultState = [];

export default function (state = defaultState, action) {
  if (action.type === TasksLoaded.type) {
    return action.payload;
  }

  if (action.type === TaskDelete.type) {
    return state.filter(t => t.id !== action.payload);
  }

  if (action.type === LOCATION_CHANGE) {
    return defaultState;
  }

  return state;
}

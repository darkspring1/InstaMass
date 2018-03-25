

import { LOCATION_CHANGE, TagTaskCreateOrUpdate } from 'actions';

const defaultState = null;

export default function tagTask(state = defaultState, action) {
  if (action.type === TagTaskCreateOrUpdate.type) {
    return action.payload;
  }

  if (action.type === LOCATION_CHANGE) {
    return defaultState;
  }

  return state;
}

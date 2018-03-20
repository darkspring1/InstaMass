
import * as Actions from '../actions';

export default function tagTask(state = null, action) {
  if (action.type === Actions.TagTaskCreateOrUpdate.type) {
    return action.payload;
  }

  return state;
}

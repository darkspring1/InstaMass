
import * as Actions from '../actions';

export default function tagTask(state = null, action) {
  if (action.type === Actions.TagTaskCreated.type ||
    action.type === Actions.TagTaskLoaded.type
  ) {
    return action.payload;
  }

  return state;
}

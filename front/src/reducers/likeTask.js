
import ActionTypes from './../constants/actionTypes';

export default function likeTask(state = {}, action) {
  if (action.type === ActionTypes.ADD_NEW_TAG_SUCCEEDED) {
    const tags = state.tags || [];
    tags.push(action.payload);
    return {
      ...state, tags
    };
  }

  return state;
}


import ActionTypes from './../constants/actionTypes';

export default function preloader(state = {}, action) {
  if (action.type === ActionTypes.REQUEST_STARTED) {
    const result = { ...state };
    let counter = result[action.payload.preloader];
    if (counter === undefined) {
      counter = 0;
    }
    counter += 1;
    result[action.payload.preloader] = counter;
    return result;
  } else if (action.type === ActionTypes.REQUEST_FINISHED) {
    const result = { ...state };
    let counter = result[action.payload.preloader];
    counter -= 1;
    result[action.payload.preloader] = counter;
    return result;
  }
  return state;
}

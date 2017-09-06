

import ActionTypes from './../constants/actionTypes';

import { AddNewAccount } from './../api';

function action(newAccount) {
  debugger;
  return AddNewAccount(newAccount)
  .then((response) => {
    debugger;
    return {
      type: ActionTypes.NEW_ACCOUNT,
      payload: response
    };
  });
}

export default newAccount => /* dispatch */() => action(newAccount);

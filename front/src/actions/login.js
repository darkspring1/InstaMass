/* eslint max-len: 0  */

import { Login } from '../api/';


function action(loginData/* , dispatch */) {
  debugger;
  return Login(loginData.email, loginData.password)
  .then((/* response */) => {
    debugger;
  });
}

export default loginData => dispatch => action(loginData, dispatch);

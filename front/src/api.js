import Fetch from 'isomorphic-fetch';
import Settings from './settings';

function RegisterExternal(registerExternalData) {
  return Fetch(`${Settings.ApiServiceBaseUri}api/account/registerexternal`, {
    method: 'POST',
    body: JSON.stringify(registerExternalData),
    headers: {
      'Content-Type': 'application/json'
    },
  });
}


function test() {

}

export { RegisterExternal, test };

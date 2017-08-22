import Fetch from 'isomorphic-fetch';
import Settings from './settings';

function RegisterExternal(registerExternalData) {
  return Fetch(`${Settings.ApiServiceBaseUri}api/account/registerexternal`, {
    method: 'POST',
    body: JSON.stringify(registerExternalData),
    headers: {
      'Content-Type': 'application/json'
    },
  }).then(response => response.json());
}


function ObtainLocalAccessToken(externalData) {
  const url = new URL(`${Settings.ApiServiceBaseUri}api/account/ObtainLocalAccessToken`);
  Object.keys(externalData).forEach(key => url.searchParams.append(key, externalData[key]));
  return Fetch(url).then(response => response.json());
}

export { RegisterExternal, ObtainLocalAccessToken };

/* eslint no-param-reassign: 0 */
import axios from 'axios';
import LocalStorage from '../localStorage';

const regex = /^.*\/api\/.*$/g;

const authInterceptor = (config) => {
  if ((regex.exec(config.url)) !== null) {
    debugger;
    // This is necessary to avoid infinite loops with zero-width matches
    const authData = LocalStorage.get('authorizationData');
    if (authData) {
      config.headers.Authorization = `Bearer ${authData.token}`;
    }
  }
  return config;
}
;


export default function add() {
  if (!axios.interceptors.request.handlers.find(h => h.fulfilled.name === authInterceptor.name)) {
    axios.interceptors.request.use(authInterceptor);
  }
}

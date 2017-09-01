/* eslint max-len: 0  */
/* eslint no-unused-vars: 0  */
import { push } from 'react-router-redux';
import AuthSettings from './../settings';
import ActionTypes from './../constants/actionTypes';
import LocalStorageKeys from './../constants/localStorageKeys';
import { RegisterExternal, ObtainLocalAccessToken, LoginExternal } from './../api';
import LocalStorage from '../localStorage';


export default provider => (dispatch) => {
  const redirectUri = `${location.protocol}//${location.host}/authcomplete.html`;
  /* не переводить строку */
  const externalProviderUrl = `${AuthSettings.apiServiceBaseUri}api/user/ExternalLogin?provider=${provider}&response_type=token&client_id=${AuthSettings.clientId}&redirect_uri=${redirectUri}`;

  window.dispatchAuthExternalProvider = (fragment) => {
    // dispatch({ type: ActionNames.AUTH_EXTERNAL_PROVIDER, payload });
    const externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
    if (fragment.haslocalaccount === 'False') {
      externalData.userName = fragment.external_user_name;
      RegisterExternal(externalData)
      .then((response) => {
        debugger;
      })
      .catch((err) => {
        debugger;
      });
    } else {
      LoginExternal({ ...externalData, appId: AuthSettings.clientId }).then((response) => {
        dispatch({ type: ActionTypes.AUTHORIZATION_DATA, payload: response.data });
        dispatch(push('/dashboard'));
      })
      .catch((err) => { console.log(err); });
    }
  };


  window.open(externalProviderUrl, 'Authenticate Account', 'location=0,status=0,width=600,height=750');
};

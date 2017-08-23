/* eslint max-len: 0  */
/* eslint no-unused-vars: 0  */
import { push } from 'react-router-redux';
import AuthSettings from './../settings';
import ActionTypes from './../constants/actionTypes';
import LocalStorageKeys from './../constants/localStorageKeys';

import { RegisterExternal, ObtainLocalAccessToken, Orders } from './../api';
import LocalStorage from '../localStorage';
import AddAuthInterceptor from '../interceptors/authInterceptor';

export default provider => (dispatch) => {
  const redirectUri = `${location.protocol}//${location.host}/authcomplete.html`;
  /* не переводить строку */
  const externalProviderUrl = `${AuthSettings.ApiServiceBaseUri}api/Account/ExternalLogin?provider=${provider}&response_type=token&client_id=${AuthSettings.ClientId}&redirect_uri=${redirectUri}`;

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
                // Obtain access token and redirect to orders
      ObtainLocalAccessToken(externalData).then((response) => {
        LocalStorage.set(LocalStorageKeys.AUTHORIZATION_DATA, { token: response.data.access_token, userName: response.data.userName, refreshToken: '', useRefreshTokens: false });
        AddAuthInterceptor();
        dispatch({ type: ActionTypes.AUTHORIZATION_DATA, payload: { user: response.data, isAuth: true } });
        dispatch(push('/dashboard'));
      })
      .catch((err) => { console.log(err); });
    }
  };


  window.open(externalProviderUrl, 'Authenticate Account', 'location=0,status=0,width=600,height=750');
};

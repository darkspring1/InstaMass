/* eslint max-len: 0  */
/* eslint no-unused-vars: 0  */

import AuthSettings from './../settings';
// import ActionNames from './../actionNames';
import { RegisterExternal } from './../api';

export default provider => (/* dispatch */) => {
  const redirectUri = `${location.protocol}//${location.host}/authcomplete.html`;
  /* не переводить строку */
  const externalProviderUrl = `${AuthSettings.ApiServiceBaseUri}api/Account/ExternalLogin?provider=${provider}&response_type=token&client_id=${AuthSettings.ClientId}&redirect_uri=${redirectUri}`;

  // RegisterExternal(JSON.parse('{"provider":"Facebook","userName":"Egor Ol\'t","externalAccessToken":"EAACys2SmZBUwBANA8p8iQZCHLdUPGnSaoYu5ZBZCVcu5GNqugpJduYuV2JRLqlvRfpNDpAqJJu8eFuUNQUI5ZAPmnS9AmwW6n5cylpTSmmfU9rZCaHZBzRXug0SHgZAFRrYIjW0miAIwHOTvbrpoSrVn2ehmrtMCm4Y1OtexZBeiZCcAZDZD"}'));

  window.dispatchAuthExternalProvider = (fragment) => {
    // dispatch({ type: ActionNames.AUTH_EXTERNAL_PROVIDER, payload });

    if (fragment.haslocalaccount === 'False') {
      const p = RegisterExternal({
        provider: fragment.provider,
        userName: fragment.external_user_name,
        externalAccessToken: fragment.external_access_token
      });
    }
    /*
    else {
                // Obtain access token and redirect to orders
      const externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
      authService.obtainAccessToken(externalData).then((response) => {
        $location.path('/orders');
      },
             (err) => {
               $scope.message = err.error_description;
             });
    }
            */
  };

  window.open(externalProviderUrl, 'Authenticate Account', 'location=0,status=0,width=600,height=750');
};

import AuthSettings from './../authSettings';

export default provider => (dispatch) => {
  debugger;
  const redirectUri = `${location.protocol}//${location.host}/authcomplete.html`;
  const externalProviderUrl = `${AuthSettings.ApiServiceBaseUri}api/Account/ExternalLogin?provider=${provider}
  &response_type=token&client_id=${AuthSettings.ClientId}&redirect_uri=${redirectUri}`;

  window.dispatchAuthExternalProvider = (payload) => {
    debugger;
    dispatch({ type: 'AUTH_EXTERNAL_PROVIDER', payload });
  };

  window.open(externalProviderUrl, 'Authenticate Account', 'location=0,status=0,width=600,height=750');
};

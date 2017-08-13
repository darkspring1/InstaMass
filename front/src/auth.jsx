/* eslint class-methods-use-this: 0 */

import React from 'react';
import { connect } from 'react-redux';
import AuthSettings from './authSettings';


class Auth extends React.Component {

  constructor(props) {
    super(props);
    this.authExternalProvider = this.authExternalProvider.bind(this);
  }

  componentWillMount() {
    const head = document.getElementsByTagName('head')[0];
    const element = document.createElement('link');         // Create a text node
    element.setAttribute('rel', 'stylesheet');
    element.setAttribute('href', 'app/auth.css');
    head.appendChild(element);
    this.style = element;
  }

  componentWillUnmount() {
    debugger;
    this.style.parentNode.removeChild(this.style);
  }

  authExternalProvider(provider) {
    this.props.onAuthExternalProvider(provider);
  }


  render() {
    return (<main className="auth-main">
      <div className="auth-block">
        <h1>Sign in to Blur Admin</h1>
        <a href="reg.html" className="auth-link">New to Blur Admin? Sign up!</a>

        <form className="form-horizontal">
          <div className="form-group">
            <label htmlFor="inputEmail3" className="col-sm-2 control-label">Email</label>

            <div className="col-sm-10">
              <input type="email" className="form-control" id="inputEmail3" placeholder="Email" />
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="inputPassword3" className="col-sm-2 control-label">Password</label>

            <div className="col-sm-10">
              <input type="password" className="form-control" id="inputPassword3" placeholder="Password" />
            </div>
          </div>
          <div className="form-group">
            <div className="col-sm-offset-2 col-sm-10">
              <button type="submit" className="btn btn-default btn-auth">Sign in</button>
              <a href className="forgot-pass">Forgot password?</a>
            </div>
          </div>
        </form>

        <div className="auth-sep"><span><span>or Sign in with one click</span></span></div>

        <div className="al-share-auth">
          <ul className="al-share clearfix">
            <li>
              <a role="button" tabIndex={0} onClick={() => this.authExternalProvider('Facebook')}>
                <i className="socicon socicon-facebook" title="Share on Facebook" />
              </a>
            </li>
            <li><i className="socicon socicon-twitter" title="Share on Twitter" /></li>
            <li><i className="socicon socicon-google" title="Share on Google Plus" /></li>
          </ul>
        </div>
      </div>
    </main>);
  }
}


export default connect(
  state => ({ state }), // map state to props
  dispatch => ({
    onAuthExternalProvider(provider) {
      debugger;
      const redirectUri = `${location.protocol}//${location.host}/authcomplete.html`;

      const externalProviderUrl = `${AuthSettings.ApiServiceBaseUri}api/Account/ExternalLogin?provider=${provider
}&response_type=token&client_id=${AuthSettings.ClientId
}&redirect_uri=${redirectUri}`;
    // window.$windowScope = $scope;


    /* const oauthWindow = */
      window.open(externalProviderUrl, 'Authenticate Account', 'location=0,status=0,width=600,height=750');

      dispatch({ type: 'AUTH_EXTERNAL_PROVIDER' });
    }
  })
)(Auth);

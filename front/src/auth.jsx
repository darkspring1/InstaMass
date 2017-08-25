/* eslint class-methods-use-this: 0 */

import React from 'react';
import { connect } from 'react-redux';

import AuthExternalProvider from './actions/authExternalProvider';
import Login from './actions/login';

class Auth extends React.Component {

  constructor(props) {
    super(props);
    this.state = { email: 'olt.egor@gmail.com', password: '123' };
    this.login = this.login.bind(this);
    this.emailHandleChange = this.emailHandleChange.bind(this);
    this.passwordHandleChange = this.passwordHandleChange.bind(this);
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
    this.style.parentNode.removeChild(this.style);
  }

  login(event) {
    this.props.onLogin(this.state);
    event.preventDefault();
  }

  emailHandleChange(event) {
    this.setState({ email: event.target.value });
  }

  passwordHandleChange(event) {
    this.setState({ password: event.target.value });
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
              <input
                type="email"
                className="form-control"
                value={this.state.email}
                onChange={this.emailHandleChange}
                placeholder="Email"
              />
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="inputPassword3" className="col-sm-2 control-label">Password</label>

            <div className="col-sm-10">
              <input
                type="password"
                className="form-control"
                value={this.state.password}
                onChange={this.passwordHandleChange}
                placeholder="Password"
              />
            </div>
          </div>
          <div className="form-group">
            <div className="col-sm-offset-2 col-sm-10">
              <button
                className="btn btn-default btn-auth"
                onClick={this.login}
              >Sign in</button>
              <a href className="forgot-pass">Forgot password?</a>
            </div>
          </div>
        </form>

        <div className="auth-sep"><span><span>or Sign in with one click</span></span></div>

        <div className="al-share-auth">
          <ul className="al-share clearfix">
            <li>
              <a role="button" tabIndex={0} onClick={() => this.props.onAuthExternalProvider('Facebook')}>
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
    dispatch(AuthExternalProvider(provider));
  },

  onLogin(loginData) {
    debugger;
    dispatch(Login(loginData));
  }
})
)(Auth);

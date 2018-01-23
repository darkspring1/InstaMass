/* eslint class-methods-use-this: 0 */
/* eslint jsx-a11y/label-has-for: 0 */


import React from 'react';
import { connect } from 'react-redux';
import { Field, reduxForm } from 'redux-form';

// import { Login, AuthExternalProvider } from 'actions';
import { required } from 'form-validators';
import { SignIn, SignInExternal } from 'actions';
import renderField from './renderField';

const formName = 'auth';

const emailRequered = required('Введите Email');
const passwordRequered = required('Введите пароль');


class Auth extends React.Component {

  constructor(props) {
    super(props);
    this.login = this.login.bind(this);
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

  login(v, event) {
    if (this.props.valid) {
      this.props.onLogin(this.props.fields);
      event.preventDefault();
    }
  }


  render() {
    const { /* handleSubmit, */pristine, /* reset, */ submitting } = this.props;
    return (<main className="auth-main">
      <div className="auth-block">
        <h1>Sign in to Blur Admin</h1>
        <a href="reg.html" className="auth-link">New to Blur Admin? Sign up!</a>

        <div className="form-horizontal">
          <form >

            <Field
              name="email"
              component={renderField}
              label="Email"
              type="email"
              validate={[emailRequered]}
            />

            <Field
              name="password"
              component={renderField}
              label="Password"
              type="password"
              validate={[passwordRequered]}
            />
          </form>
          <div className="form-group">
            <div className="col-sm-offset-2 col-sm-10">
              <button
                onClick={this.login}
                disabled={pristine || submitting}
                className="btn btn-default btn-auth"
              >Sign in</button>
              <a href className="forgot-pass">Forgot password?</a>
            </div>
          </div>
        </div>

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


const authForm = reduxForm({
  // a unique name for the form
  form: formName
})(Auth);


export default connect(
  (state) => {
    const values = state.form[formName] ? state.form[formName].values : null;
    return { fields: values, auth: state.auth };
  },
dispatch => ({
  onAuthExternalProvider(provider) {
    debugger;
    dispatch(SignInExternal(provider));
    // dispatch(AuthExternalProvider(provider));
  },

  onLogin(loginData) {
    debugger;
    dispatch(SignIn(loginData));
    // dispatch(Login(loginData));
  }
})
)(authForm);

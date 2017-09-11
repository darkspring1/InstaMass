import React from 'react';
import { Route, Redirect, withRouter, Switch } from 'react-router-dom';
import { connect } from 'react-redux';

import Layout from './components/layout';
import Auth from './components/auth';


function App(props) {
  let redirectDefault;
  const authPath = '/auth';
  if (props.state.auth.isAuth) {
    if (location.pathname === '/') {
      redirectDefault = '/accounts';
    }
  } else if (location.pathname !== authPath) { redirectDefault = authPath; }

  let redirect = null;
  if (redirectDefault) {
    redirect = <Redirect from="/" exact to={redirectDefault} />;
  }

  return (
    <div>
      {redirect}
      <Switch>
        <Route exact path={authPath} component={Auth} />
        <Route path="/" component={Layout} />

      </Switch>
    </div>
  );
}


const app = connect(
  state => ({ state }), // map state to props
  // dispatch => ({
  //   goToNewAccount() {
  //     dispatch(push('account'));
  //   }

  // })
)(App);

export default withRouter(app);

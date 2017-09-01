import React from 'react';
import { Route, Redirect, withRouter, Switch } from 'react-router-dom';
import { connect } from 'react-redux';

import Layout from './components/layout';
import Auth from './components/auth';


function App(props) {
  const redirectDefault = props.state.auth.isAuth ? '/dashboard' : '/auth';

  return (
    <div>
      <Redirect from="/" exact to={redirectDefault} />
      <Switch>
        <Route exact path="/auth" component={Auth} />
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

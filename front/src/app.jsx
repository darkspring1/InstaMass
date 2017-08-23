import React from 'react';
import { Route, Redirect, withRouter, Switch } from 'react-router-dom';
import { connect } from 'react-redux';

import Layout from './layout';
import Auth from './auth';


function App(props) {
  debugger;
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
  // dispatch => ({})
)(App);

export default withRouter(app);

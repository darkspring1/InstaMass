import React from 'react';
import { BrowserRouter as Router, Route, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import Layout from './layout';
import Auth from './auth';


function App() {
  const isAuth = true;
  let redirectDefault = '/auth';
  if (isAuth) {
    redirectDefault = '/dashboard';
  }
  return (
    <Router>
      <div>
        <Redirect from="/" exact to={redirectDefault} />
        <Route exact path="/auth" component={Auth} />
        <Route path="/" component={Layout} />
      </div>
    </Router>
  );
}


export default connect(
  state => ({ state }), // map state to props
  // dispatch => ({})
)(App)
;

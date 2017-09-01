/* eslint jsx-a11y/anchor-has-content: 0 */
import React from 'react';
import { Route, Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { push } from 'react-router-redux';
import Dashboard from './dashboard';
import AccountEditor from './accountEditor';

function Layout(props) {
  return (
    <main>
      <div className="body-bg" />
      <aside className="al-sidebar">
        <div className="slimScrollDiv">
          <ul className="al-sidebar-list">

            <li className="al-sidebar-list-item selected" >
              <Link className="al-sidebar-list-link" to="/dashboard">
                <i className="ion-android-home" />
                <span>Dashboard_</span>
              </Link>
            </li>

            <li className="al-sidebar-list-item with-sub-menu">
              <a className="al-sidebar-list-link">
                <i className="ion-gear-a" />
                <span >Components</span>
                <b className="fa fa-angle-down" />
              </a>

              <ul className="al-sidebar-sublist">
                <li className="ba-sidebar-sublist-item">
                  <a target="_self" href="#/components/mail">Mail</a>
                </li>
              </ul>
            </li>
          </ul>

        </div>
      </aside>

      <div className="page-top clearfix" >
        <a href="#/dashboard" className="al-logo clearfix"><span>Blur</span>Admin</a>
        <a className="collapse-menu-link ion-navicon" />

        <div className="search">
          <i className="ion-ios-search-strong" />
          <input id="searchInput" type="text" placeholder="Search for..." />
        </div>
      </div>

      <div className="al-main">
        <div className="al-content">
          <Route path="/dashboard" component={() => <Dashboard goToNewAccount={props.goToNewAccount} />} />
          <Route path="/account" component={AccountEditor} />
        </div>
      </div>

    </main>
  );
}


const layout = connect(
  state => ({ state }), // map state to props
  dispatch => ({
    goToNewAccount() {
      dispatch(push('account'));
    }

  })
)(Layout);

export default layout;

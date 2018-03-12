/* eslint jsx-a11y/anchor-has-content: 0 */
import React from 'react';
import { Route, Link, Switch } from 'react-router-dom';
import { connect } from 'react-redux';
import AccountList from './accountList';
import TaskList from './taskList';
import AccountEditor from './accountEditor';
import TopPreloader from './topPreloader';
import Toastr from './toastr';
import TagTaskEditor from './tagTaskEditor/editor';
import * as Routes from '../constants/routes';
import TaskTypes from './taskTypes';

function Layout(/* props */) {
  return (
    <main>
      <Toastr />
      <div className="body-bg" />
      <aside className="al-sidebar">
        <div className="slimScrollDiv">
          <ul className="al-sidebar-list">

            <li className="al-sidebar-list-item selected" >
              <Link className="al-sidebar-list-link" to="/accounts">
                <i className="ion-android-home" />
                <span>Мои аккаунты</span>
              </Link>
            </li>

            <li className="al-sidebar-list-item selected" >
              <Link className="al-sidebar-list-link" to="/tasks">
                <i className="ion-android-home" />
                <span>Задачи</span>
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
        <TopPreloader />
        <div className="al-content">
          <Route path="/accounts" component={AccountList} />


          <Switch>
            <Route path={Routes.TAG_TASK_EDITOR} component={TagTaskEditor} />
            <Route path={Routes.TASK_TYPES} component={TaskTypes} />
            <Route path={Routes.TASK_LIST} component={TaskList} />
          </Switch>


          <Route path="/account" component={AccountEditor} />
        </div>
      </div>

    </main>
  );
}


const layout = connect(
  state => ({ state }), // map state to props
)(Layout);

export default layout;

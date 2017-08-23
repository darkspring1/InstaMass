/* eslint no-unused-vars: 0 */
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import { ConnectedRouter, routerReducer, routerMiddleware/* , push */ } from 'react-router-redux';
import { Route } from 'react-router-dom';
import createHistory from 'history/createBrowserHistory';
import thunk from 'redux-thunk';

import App from './app';
import * as Reducers from './reducers';

import ActionTypes from './constants/actionTypes';
import LocalStorageKeys from './constants/localStorageKeys';
import LocalStorage from './localStorage';

const history = createHistory();
const routerMW = routerMiddleware(history);

const reducers = combineReducers({
  ...Reducers,
  router: routerReducer
});

const store = createStore(reducers, composeWithDevTools(applyMiddleware(routerMW, thunk)));

debugger;
const authData = LocalStorage.get(LocalStorageKeys.AUTHORIZATION_DATA);
if (authData) {
  store.dispatch({ type: ActionTypes.AUTHORIZATION_DATA, payload: { user: authData, isAuth: true } });
}

ReactDOM.render(
  <Provider store={store}>

    <ConnectedRouter history={history}>
      <div>
        <App />
      </div>
    </ConnectedRouter>
  </Provider>,
  document.getElementById('root')
);

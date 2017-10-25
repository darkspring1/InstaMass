/* eslint no-unused-vars: 0 */
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import { reducer as formReducer } from 'redux-form';
import { composeWithDevTools } from 'redux-devtools-extension';
import { ConnectedRouter, routerReducer, routerMiddleware/* , push */ } from 'react-router-redux';
import { Route } from 'react-router-dom';
import createHistory from 'history/createBrowserHistory';
import thunk from 'redux-thunk';
import createSagaMiddleware from 'redux-saga';
import rootSaga from './sagas';

import App from './app';
import * as Reducers from './reducers';
import ActionTypes from './constants/actionTypes';
import LocalStorageKeys from './constants/localStorageKeys';
import LocalStorage from './localStorage';
import AddAuthInterceptor from './interceptors/authInterceptor';


const history = createHistory();
const routerMW = routerMiddleware(history);
const sagaMiddleware = createSagaMiddleware();

const reducers = combineReducers({
  ...Reducers,
  router: routerReducer,
  form: formReducer
});

const store = createStore(reducers, composeWithDevTools(applyMiddleware(routerMW, thunk, sagaMiddleware)));

sagaMiddleware.run(rootSaga);

AddAuthInterceptor(store);
const authData = LocalStorage.get(LocalStorageKeys.AUTHORIZATION_DATA);
if (authData) {
  store.dispatch({ type: ActionTypes.AUTHORIZATION_DATA, payload: authData });
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

/* eslint jsx-a11y/label-has-for: 0 */
/* eslint react/prefer-stateless-function: 0 */
/* eslint no-useless-constructor: 0 */
/* eslint jsx-a11y/role-has-required-aria-props: 0 */


import React from 'react';
import { connect } from 'react-redux';

import ContentTop from '../components/contentTop';
import TagInfo from '../components/tagInfo';
import Button from '../controls/button';
import ActionTypes from '../constants/actionTypes';

// import AddNewAccount from '../actions/addNewAccount';

class LikeTaskEditor extends React.Component {

  constructor(props) {
    super(props);
    this.addNewAccount = this.addNewAccount.bind(this);
    this.loginHandleChange = this.loginHandleChange.bind(this);
    this.passwordHandleChange = this.passwordHandleChange.bind(this);
    this.state = { login: '', password: '' };
  }

  addNewAccount() {
    this.props.onAddNewAccount(this.state);
  }

  loginHandleChange(event) {
    this.setState({ login: event.target.value });
  }

  passwordHandleChange(event) {
    this.setState({ password: event.target.value });
  }


  render() {
    return (
      <div>
        <ContentTop title="Новая задача" />

        <div
          className="panel profile-page animated zoomIn"
          style={{ backgroundSize: '1359px 764px', backgroundPosition: '0px -224px' }}
        >
          <div className="panel-body" >
            <div className="panel-content">
              <h3 className="with-line">General Information</h3>

              <div className="row">
                <div className="form-group col-sm-3 col-xs-6">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Добавить новый хэштег"
                    onChange={this.passwordHandleChange}
                    value={this.state.password}
                  />
                </div>

                <div className="form-group col-sm-3 col-xs-6">
                  <Button text="Добавить" primary />
                </div>

              </div>

              <div className="row">
                <div className="col-md-3">
                  <TagInfo tag="tits" total="100500" />
                </div>
              </div>

              <Button text="Сохранить" onClick={this.addNewAccount} success large />

            </div>
          </div>
        </div>
      </div>

    );
  }

}


const likeTaskEditor = connect(
  state => ({ state }), // map state to props
  dispatch => ({
    onAddNewAccount(newAccount) {
      dispatch({ type: ActionTypes.ADD_NEW_ACCOUNT_REQUESTED, payload: newAccount });
    }

  })
)(LikeTaskEditor);

export default likeTaskEditor;

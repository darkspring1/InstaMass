/* eslint jsx-a11y/label-has-for: 0 */


import React from 'react';
import { connect } from 'react-redux';

import AccountPanel from '../components/accountPanel';
import ContentTop from '../components/contentTop';
import { /* AddNewAccountRequested, */AccountsRequested } from '../actions';


class TaskList extends React.Component {

  constructor(props) {
    super(props);
    this.onAddAccount = this.onAddAccount.bind(this);
  }


  onAddAccount(/* e */) {
    this.props.goToNewAccount();
    // console.log(e + this);
  }

  render() {
    const props = this.props;

    if (!props.state) {
      props.onAccountsRequested();
      return null;
    }

    if (props.state === 'loading') {
      return null;
    }

    return (
      <div>

        <ContentTop title="Аккаунты" />

        <div className="row">

          <div className="col-md-12">
            <div className="panel with-scroll animated zoomIn">
              <div className="panel-heading clearfix"><h3 className="panel-title">Inline Form</h3>
              </div>
              <div className="panel-body">

                <div>
                  <form className="row form-inline">
                    <div className="form-group col-sm-3 col-xs-6">
                      <input type="text" className="form-control" id="exampleInputName2" placeholder="Name" />
                    </div>
                    <div className="form-group col-sm-3 col-xs-6">
                      <input type="email" className="form-control" id="exampleInputEmail2" placeholder="Email" />
                    </div>
                    <div className="checkbox">
                      <label className="custom-checkbox">
                        <input type="checkbox" />
                        <span>Remember me</span>
                      </label>
                    </div>
                    <button
                      type="submit"
                      className="btn btn-primary"
                      onClick={this.onAddAccount}
                    >Добавить Аккаунт</button>
                  </form>
                </div>
              </div>
            </div>
          </div>

          <div className="col-md-12"><AccountPanel /></div>

        </div>

      </div>);
  }

}

function stateToProps(state) {
  return state.account.accounts || {};
}


const taskList = connect(
  stateToProps,
  dispatch => ({
    // onAddNewAccount(newAccount) {
    //   dispatch(AddNewAccountRequested(newAccount));
    // },
    onAccountsRequested() {
      dispatch(AccountsRequested());
    }
  })
)(TaskList);

export default taskList;


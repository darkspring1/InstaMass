/* eslint jsx-a11y/label-has-for: 0 */

import React from 'react';
// import { Orders } from './api';
import AccountPanel from './components/accountPanel';

export default class Dashboard extends React.Component {

  constructor(props) {
    super(props);

    this.onAddAccount = this.onAddAccount.bind(this);
  }


  onAddAccount(e) {
    debugger;
    console.log(e + this);
  }

  // Orders();

  render() {
    return (
      <div>
        <div className="content-top clearfix">
          <h1 className="al-title">Dashboard</h1>

          <ul className="breadcrumb al-breadcrumb">
            <li>
              <a href="#/dashboard">Home</a></li>
            <li>Dashboard</li>
          </ul>
        </div>

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

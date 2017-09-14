/* eslint jsx-a11y/label-has-for: 0 */
/* eslint react/prefer-stateless-function: 0 */


import React from 'react';
import { connect } from 'react-redux';
import { push } from 'react-router-redux';
import ContentTop from '../components/contentTop';
import * as Routes from '../constants/routes';

class TaskList extends React.Component {


  render() {
    // const props = this.props;

    return (
      <div>

        <ContentTop title="Выберите задачу" />

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
                      onClick={this.props.goToNewTask}
                    >Создать задачу</button>
                  </form>
                </div>
              </div>
            </div>
          </div>

          <div className="col-md-12">
            todo: task list
          </div>

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
    goToNewTask() {
      dispatch(push(Routes.TASK_TYPES));
    },


  })
)(TaskList);

export default taskList;


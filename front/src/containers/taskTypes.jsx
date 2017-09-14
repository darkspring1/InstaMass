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

        <div className="row pie-charts">

          <div className="pie-chart-item-container">

            <div className="panel animated zoomIn">
              <div className="panel-body" >
                <div className="pie-chart-item">
                  <div className="description">
                    <div>New Visits</div>
                    <div className="description-stats">57,820</div>
                  </div>
                  <i className="chart-icon i-person" />
                </div>
              </div>
            </div>

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


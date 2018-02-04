/* eslint jsx-a11y/label-has-for: 0 */
/* eslint react/prefer-stateless-function: 0 */
/* eslint jsx-a11y/anchor-has-content: 0 */


import React from 'react';
import { connect } from 'react-redux';
import { push } from 'react-router-redux';
import { TasksRequested } from 'actions';
import ContentTop from '../components/contentTop';
import * as Routes from '../constants/routes';

class TaskList extends React.Component {

  componentWillMount() {
    this.props.onTasksRequested();
  }

  render() {
    const tasks = this.props.tasks;

    const taskRows = tasks.map(t => (<tr className="editable-row">
      <td>
        <table>
          <tbody>
            <tr>
              <td className="little-human photo-td">
                <img alt="_" className="little-human-picture" src="assets/img/app/profile/Kostya.png" />
              </td>
              <td>
                <div className="name-container">
                  <div>
                    <span className="name">{t.account.login}</span>
                  </div>
                  <div>
                    <span className="tag label label-primary family">family</span>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </td>
      <td className="select-td">
        <span className="editable">Likes</span>
      </td>
      <td className="select-td">
        <span className="editable">Активна</span>
      </td>
      <td>
        <form name="rowform" className="form-buttons form-inline">
          <button type="submit" className="btn btn-primary editable-table-button btn-xs">Save</button>
          <button
            type="button"
            className="btn btn-default editable-table-button btn-xs"
          >Cancel</button>
        </form>
        <div className="buttons">
          <button className="btn btn-primary editable-table-button btn-xs">Edit</button>
          <button className="btn btn-danger editable-table-button btn-xs">Delete</button>
        </div>
      </td>
    </tr>));

    return (
      <div>

        <ContentTop title="Задачи" />

        <div className="row">

          <div className="col-md-12">
            <div
              className="panel panel-blur with-scroll animated zoomIn"
              style={{ backgroundSize: '1359px 764px', backgroundPosition: '0px -203px' }}
            >
              <div className="panel-body" >

                <div className="add-row-editable-table">
                  <button className="btn btn-primary">Создать задачу</button>
                </div>
                <table className="table table-bordered table-hover table-condensed">
                  <tbody>
                    <tr>
                      <td>Аккаунт</td>
                      <td>Тип</td>
                      <td>Статус</td>
                      <td>Дейсвия</td>
                    </tr>
                    {taskRows}

                  </tbody>
                </table>

              </div>
            </div>
          </div>

        </div>

      </div>);
  }

}

function stateToProps(state) {
  let tasks = [];
  if (state.task) {
    tasks = state.task;
  }
  return { tasks };
}


const taskList = connect(
  stateToProps,
  dispatch => ({
    goToNewTask() {
      dispatch(push(Routes.TASK_TYPES));
    },

    onTasksRequested() {
      dispatch(TasksRequested());
    }

  })
)(TaskList);

export default taskList;


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

  static RenderTaskStatus(statusId) {
    if (statusId === 1) {
      return <span className="label label-primary">Выполняется</span>;
    }

    throw new Error(`unknow statusId: ${statusId}`);
  }

  static RenderTaskType(typeId) {
    if (typeId === 1) {
      return <span>Like</span>;
    }

    throw new Error(`unknow typeId: ${typeId}`);
  }

  static RenderAccountStatus(statusId) {
    if (statusId === 1) {
      return <span className="tag label label-primary">Активный</span>;
    }

    throw new Error(`unknow statusId: ${statusId}`);
  }

  componentWillMount() {
    this.props.onTasksRequested();
  }

  render() {
    const tasks = this.props.tasks;


    const taskRows = tasks.map(t => (<tr className="editable-row">
      <td>
        <table>
          <tbody>
            <tr className="side-message-navigation-item little-human shineHover family">
              <td className="little-human photo-td">
                <img alt="_" className="little-human-picture" src="assets/img/app/profile/Kostya.png" />
              </td>
              <td>
                <div className="name-container">
                  <div style={{ 'line-height': '16px' }}>
                    <span className="name">{t.account.login}</span>
                  </div>
                  <div style={{ 'line-height': '16px' }}>
                    {TaskList.RenderAccountStatus(t.account.statusId)}
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </td>
      <td className="select-td">
        {TaskList.RenderTaskType(t.typeId)}
      </td>
      <td className="select-td">
        {TaskList.RenderTaskStatus(t.statusId)}
      </td>
      <td>
        <div className="buttons">
          <button className="btn btn-primary editable-table-button btn-xs">Редактировать</button>
          <button className="btn btn-danger editable-table-button btn-xs">Удалить</button>
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
                  <button className="btn btn-primary" onClick={this.props.goToNewTask}>Создать задачу</button>
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


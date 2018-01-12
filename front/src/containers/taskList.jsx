/* eslint jsx-a11y/label-has-for: 0 */
/* eslint react/prefer-stateless-function: 0 */
/* eslint jsx-a11y/anchor-has-content: 0 */


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

        <ContentTop title="Задачи" />

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

          <div className="col-md-12" >

            <div className="row mail-client-container transparent">
              <div className="col-md-12">
                <div>
                  <div
                    className="panel panel-blur xmedium-panel mail-panel animated zoomIn"
                    style={{ background_size: '1359px 764px', background_position: '0px -240px;' }}
                  >
                    <div className="panel-body">
                      <div className="letter-layout">
                        <div className="mail-navigation-container">
                          <div className="text-center">
                            <button type="button" className="btn btn-default compose-button">Compose</button>
                          </div>
                          <div
                            className="mail-navigation active"
                            href="#/components/mail/inbox"
                          >Inbox<span className="new-mails">7</span>
                          </div>
                        </div>

                        <div className="side-message-navigation expanded">
                          <div className="mail-messages-control side-message-navigation-item">
                            <div className="toggle-navigation-container">
                              <a href="" className="collapse-navigation-link ion-navicon" />
                            </div>
                            <label className="checkbox-inline custom-checkbox nowrap">
                              <input type="checkbox" id="inlineCheckbox01" value="option1" />
                              <span className="select-all-label">Select All</span>
                            </label>
                            <button type="button" className="btn btn-icon refresh-button">
                              <i className="ion-refresh" />
                            </button>
                            <div className="btn-group dropdown">
                              <button
                                type="button"
                                className="btn more-button dropdown-toggle"
                                aria-haspopup="true"
                                aria-expanded="false"
                              >More <span className="caret" />
                              </button>
                              <ul className="dropdown-menu">
                                <li>
                                  <a href="">Action</a>
                                </li>
                                <li>
                                  <a href="">Another action</a>
                                </li>
                                <li>
                                  <a href="">Something else here</a>
                                </li>
                                <li role="separator" className="divider" />
                                <li>
                                  <a href="">Separated link</a>
                                </li>
                              </ul>
                            </div>
                          </div>
                          <div className="messages">
                            <table>
                              <tbody>

                                <tr className="side-message-navigation-item little-human shineHover family">
                                  <td className="check-td">
                                    <div className="mail-checkbox">
                                      <label className="checkbox-inline custom-checkbox nowrap">
                                        <input type="checkbox" />
                                        <span />
                                      </label>
                                    </div>
                                  </td>
                                  <td className="photo-td" href="#/components/mail/inbox/2334uudsa">
                                    <img
                                      className="little-human-picture"
                                      alt=""
                                      src="assets/img/app/profile/Kostya.png"
                                    />
                                  </td>
                                  <td href="#/components/mail/inbox/2334uudsa">
                                    <div className="name-container">
                                      <div>
                                        <span className="name">Kostya Danovsky</span>
                                      </div>
                                      <div>
                                        <span className="tag label label-primary family">family</span>
                                      </div>
                                    </div>
                                  </td>
                                  <td href="#/components/mail/inbox/2334uudsa">
                                    <div className="additional-info">
                                      <span className="subject">Street Art</span>
                                    </div>
                                  </td>
                                  <td href="#/components/mail/inbox/2334uudsa">
                                    <div className="mail-body-part">
                                    Hey John, Aliquam eu facilisis eros,
                                    </div>
                                  </td>
                                  <td className="date">
                                    <span>Nov 22 10:05</span>
                                  </td>
                                </tr>

                              </tbody>
                            </table>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
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


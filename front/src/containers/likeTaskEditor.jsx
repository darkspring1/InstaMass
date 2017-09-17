/* eslint jsx-a11y/label-has-for: 0 */
/* eslint react/prefer-stateless-function: 0 */
/* eslint no-useless-constructor: 0 */
/* eslint jsx-a11y/role-has-required-aria-props: 0 */


import React from 'react';
import { connect } from 'react-redux';

import ContentTop from '../components/contentTop';
import TagInfo from '../components/tagInfo';
import Button from '../controls/button';
import * as Actions from '../actions';
import AddNewTag from '../components/addNewTag';

class LikeTaskEditor extends React.Component {

  constructor(props) {
    super(props);
    this.onAddNewTask = this.onAddNewTask.bind(this);
    this.loginHandleChange = this.loginHandleChange.bind(this);
    this.passwordHandleChange = this.passwordHandleChange.bind(this);
    this.state = { login: '', password: '' };
  }

  onAddNewTask() {
    const tags = this.props.tags.map(tag => tag.tag);
    this.props.onAddNewTask({ tags });
  }

  loginHandleChange(event) {
    this.setState({ login: event.target.value });
  }

  passwordHandleChange(event) {
    this.setState({ password: event.target.value });
  }


  render() {
    const props = this.props;

    const tags = props.tags.map(t => <TagInfo tag={t.tag} total={t.total} />);

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

              <AddNewTag onAddBtnClick={props.onAddNewTag} />

              <div className="row">
                <div className="col-md-3" >
                  {tags}
                </div>
              </div>

              <Button text="Сохранить" onClick={this.onAddNewTask} success large />

            </div>
          </div>
        </div>
      </div>

    );
  }

}


function mapStateToProps(state) {
  return {
    tags: state.likeTask.tags || []
  };
}


const likeTaskEditor = connect(
  mapStateToProps, // map state to props
  dispatch => ({
    onAddNewTag(tag) {
      dispatch(Actions.AddNewTagRequested(tag));
    },

    onAddNewTask(task) {
      dispatch(Actions.AddNewLikeTaskRequested(task));
    }

  })
)(LikeTaskEditor);

export default likeTaskEditor;

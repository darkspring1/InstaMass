import React from 'react';
import { connect } from 'react-redux';
import { Field, reduxForm } from 'redux-form';
import { Button } from 'controls';
import * as Actions from 'actions';
import {
  ContentTop,
  TagInfo,
  AddNewTag,
  AccountDropDown,
  SwitchedLabel } from 'components';
import Logger from 'logger';
import RequiredIfEnabled from './requiredIfEnabledValidator';
import RangeRequiredIfEnabledValidator from './rangeRequiredIfEnabledValidator';
import RangeFromToValidator from './rangeFromToValidator';

import RenderSwitchedInputGroup from './renderSwitchedInputGroup';
import RenderSwitchedRange from './renderSwitchedRange';

const requiredValidationMessage = 'Заполните это поле или выключите его';
const requiredIfEnabledValidator = RequiredIfEnabled(requiredValidationMessage);
const rangeRequiredIfEnabledValidator = RangeRequiredIfEnabledValidator(requiredValidationMessage);
const rangeFromToValidator = RangeFromToValidator('Значение ОТ должно быть меньше значения ДО');


class TagTaskEditor extends React.Component {

  constructor(props) {
    super(props);
    this.onAddNewTask = this.onAddNewTask.bind(this);
    this.onAccountChange = this.onAccountChange.bind(this);
    this.onPostsChange = this.onPostsChange.bind(this);
    this.onFollowersChange = this.onFollowersChange.bind(this);
    this.onFollowingsChange = this.onFollowingsChange.bind(this);
    this.onLastPostChange = this.onLastPostChange.bind(this);
    this.onAvatarExist = this.onAvatarExist.bind(this);

    function range(from, to, disabled) {
      return { from, to, disabled };
    }

    this.state = {
      accountId: null,
      posts: range(0, 100, true),
      followers: range(0, 100, true),
      followings: range(0, 100, true),
      lastPost: { value: 0, disabled: false },
      avatarExist: true
    };
  }

  componentWillMount() {
    this.props.onAccountsRequested();
  }

  onPostsChange(posts) {
    this.setState({ posts });
  }

  onFollowersChange(followers) {
    this.setState({ followers });
  }

  onFollowingsChange(followings) {
    this.setState({ followings });
  }

  onLastPostChange(lastPost) {
    this.setState({ lastPost });
  }

  onAvatarExist(avatarExist) {
    this.setState({ avatarExist });
  }

  onAddNewTask() {
    if (this.props.valid) {
      const tags = this.props.tags.map(tag => tag.tag);
      this.props.onAddNewTask({ tags, accountId: this.state.accountId });
    } else {
      Logger.debug('form invalid');
    }
  }

  onAccountChange(accountId) {
    this.setState({ accountId });
  }

  render() {
    const props = this.props;
    const state = this.state;
    const tags = props.tags.map(t => <TagInfo tag={t.tag} total={t.total} />);
    return (
      <div>
        <ContentTop title="Новая задача" />

        <div className="panel animated zoomIn">
          <div className="panel-heading clearfix">
            <h3 className="panel-title">Аккаунт</h3>
          </div>

          <div className="panel-body" >
            <AccountDropDown
              onChange={this.onAccountChange}
              accounts={props.accounts}
              selectedAccountId={state.accountId}
              placeholder="Выберите Аккаунт"
              id="accoun-dropdown"
            />
          </div>

          <div className="panel-heading clearfix">
            <h3 className="panel-title">Хэштеги</h3>
          </div>

          <div className="panel-body" >
            <AddNewTag onAddBtnClick={props.onAddNewTag} />

            <div className="row">
              <div className="col-md-3" >
                {tags}
              </div>
            </div>
          </div>

          <div className="panel-heading clearfix">
            <h3 className="panel-title">Подписываться только если</h3>
          </div>

          <div className="panel-body" >
            <form>

              <div style={{ marginBottom: '15px' }}>
                <SwitchedLabel onChange={this.onAvatarExist} disabled={state.avatarExist} label="Если есть аватар" />
              </div>

              <Field
                name="lastPost"
                component={RenderSwitchedInputGroup}
                label="Последняя публикация была"
                inputLabel="дня назад"
                validate={[requiredIfEnabledValidator]}
                onChange={this.onLastPostChange}
                model={state.lastPost}
              />

              <Field
                name="posts"
                component={RenderSwitchedRange}
                label="Количество публикаций пользователя"
                validate={[rangeRequiredIfEnabledValidator, rangeFromToValidator]}
                onChange={this.onPostsChange}
                model={state.posts}
              />

              <Field
                name="followers"
                component={RenderSwitchedRange}
                label="Количество подписчиков пользователя"
                validate={[rangeRequiredIfEnabledValidator, rangeFromToValidator]}
                onChange={this.onFollowersChange}
                model={state.followers}
              />

              <Field
                name="followings"
                component={RenderSwitchedRange}
                label="Количество подписок"
                validate={[rangeRequiredIfEnabledValidator, rangeFromToValidator]}
                onChange={this.onFollowingsChange}
                model={state.followings}
              />

            </form>

            <Button text="Сохранить" onClick={this.onAddNewTask} success large />
          </div>


        </div>
      </div>

    );
  }

}

const formName = 'TagTaskEditorForm';

const TagTaskEditorForm = reduxForm({
  form: formName
})(TagTaskEditor);


function mapStateToProps(state) {
  return {
    tags: state.likeTask.tags || [],
    accounts: state.account || []
  };
}

export default connect(
  mapStateToProps, // map state to props
  dispatch => ({
    onAddNewTag(tag) {
      dispatch(Actions.AddNewTagRequested(tag));
    },

    onAddNewTask(task) {
      dispatch(Actions.AddNewLikeTaskRequested(task));
    },

    onAccountsRequested() {
      dispatch(Actions.AccountsRequested());
    }

  })
)(TagTaskEditorForm);

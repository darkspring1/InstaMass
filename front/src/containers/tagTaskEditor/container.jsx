/* eslint no-unused-vars: 0 */

import React from 'react';
import { connect } from 'react-redux';
import { Field, reduxForm, getFormValues } from 'redux-form';
import { Button } from 'controls';
import * as Actions from 'actions';
import { required } from 'form-validators';
import * as Routes from 'constants/routes';

import {
  ContentTop,
  SwitchedLabel } from 'components';
// import Logger from 'logger';
import RequiredIfEnabled from './requiredIfEnabledValidator';
import RangeRequiredIfEnabledValidator from './rangeRequiredIfEnabledValidator';
import RangeFromToValidator from './rangeFromToValidator';
import TagInputRequiredValidator from './tagInputRequiredValidator';
import TagInputUniqueValidator from './tagInputUniqueValidator';

import RenderSwitchedInputGroup from './renderSwitchedInputGroup';
import RenderSwitchedRange from './renderSwitchedRange';
import RenderAccountDropDown from './renderAccountDropDown';
import RenderTagsInput from './renderTagsInput';

const requiredValidationMessage = 'Заполните это поле или выключите его';
const requiredIfEnabledValidator = RequiredIfEnabled(requiredValidationMessage);
const rangeRequiredIfEnabledValidator = RangeRequiredIfEnabledValidator(requiredValidationMessage);
const rangeFromToValidator = RangeFromToValidator('Значение ОТ должно быть меньше значения ДО');
const requiredAccount = required('Выберите аккаунт');
const tagInputRequired = TagInputRequiredValidator('Введите тэги');
const tagInputUnique = TagInputUniqueValidator('Хэш-тэг "{tag_name}" уже добавлен');

function range(from, to, disabled) {
  return { from, to, disabled };
}

class TagTaskEditor extends React.Component {

  constructor(props) {
    super(props);
    this.onAddNewTask = this.onAddNewTask.bind(this);
    this.onAvatarExist = this.onAvatarExist.bind(this);

    this.state = {
      avatarExistDisabled: props.avatarExistDisabled
    };
  }

  componentWillMount() {
    if (this.props.taskId !== Routes.NEW_ITEM_ID) {
      this.props.onTagTaskRequested(this.props.taskId);
    }
    this.props.onAccountsRequested();
  }

  onAvatarExist(avatarExistDisabled) {
    this.setState({ avatarExistDisabled });
  }

  onAddNewTask() {
    const { avatarExistDisabled } = this.state;
    const { lastPost, followers, followings, posts, tagsInput, account } = this.props.formValues;
    this.props.onAddNewTask({
      lastPost,
      followers,
      followings,
      posts,
      avatarExistDisabled,
      tags: tagsInput.tags,
      accountId: account.id,
    });
  }


  render() {
    const props = this.props;
    const formValues = props.formValues || {};
    const { submitting, handleSubmit } = props;
    const state = this.state;
    return (
      <div>
        <form onSubmit={handleSubmit(this.onAddNewTask)}>
          <ContentTop title="Новая задача" />

          <div className="panel animated zoomIn">
            <div className="panel-heading clearfix">
              <h3 className="panel-title">Аккаунт</h3>
            </div>

            <div className="panel-body" >
              <Field
                name="account"
                component={RenderAccountDropDown}
                placeholder="Выберите Аккаунт"
                id="accoun-dropdown"
                validate={[requiredAccount]}
                accounts={props.accounts}
                value={formValues.account}
              />
            </div>

            <div className="panel-heading clearfix">
              <h3 className="panel-title">Хэштеги</h3>
            </div>

            <div className="panel-body" >
              <Field
                placeholder="Добавить тэг"
                name="tagsInput"
                component={RenderTagsInput}
                value={formValues.tagsInput}
                validate={[tagInputRequired, tagInputUnique]}
              />
            </div>

            <div className="panel-heading clearfix">
              <h3 className="panel-title">Подписываться только если</h3>
            </div>

            <div className="panel-body" >

              <div style={{ marginBottom: '15px' }}>
                <SwitchedLabel
                  onChange={this.onAvatarExist}
                  disabled={state.avatarExistDisabled}
                  label="Если есть аватар"
                />
              </div>

              <Field
                name="lastPost"
                component={RenderSwitchedInputGroup}
                label="Последняя публикация была"
                inputLabel="дня назад"
                validate={[requiredIfEnabledValidator]}
                value={formValues.lastPost}
              />

              <Field
                name="posts"
                component={RenderSwitchedRange}
                label="Количество публикаций пользователя"
                validate={[rangeRequiredIfEnabledValidator, rangeFromToValidator]}
                value={formValues.posts}
              />

              <Field
                name="followers"
                component={RenderSwitchedRange}
                label="Количество подписчиков пользователя"
                validate={[rangeRequiredIfEnabledValidator, rangeFromToValidator]}
                value={formValues.followers}
              />

              <Field
                name="followings"
                component={RenderSwitchedRange}
                label="Количество подписок"
                validate={[rangeRequiredIfEnabledValidator, rangeFromToValidator]}
                value={formValues.followings}
              />

              <Button disabled={submitting} type="submit" text="Сохранить" success large />

            </div>

          </div>
        </form>
      </div>

    );
  }

}

const formName = 'TagTaskEditorForm';

const TagTaskEditorForm = reduxForm({
  form: formName
})(TagTaskEditor);

const initialValues = {
  tagsInput: { tags: [/* 'tag1', 'tag2' */], value: '' },
  posts: range(0, 100, false),
  followers: range(0, 100, true),
  followings: range(0, 100, true),
  account: null,
  lastPost: { value: 0, disabled: false },
};

function mapStateToProps(state, ownProps) {
  return {
    taskId: ownProps.match.params.id,
    tags: state.likeTask.tags || [],
    accounts: state.account || [],
    avatarExistDisabled: false,
    initialValues,
    formValues: getFormValues(formName)(state)
  };
}

export default connect(
  mapStateToProps, // map state to props
  dispatch => ({
    onAddNewTag(tag) {
      dispatch(Actions.AddNewTagRequested(tag));
    },

    onAddNewTask(task) {
      dispatch(Actions.AddNewTagTaskRequested(task));
    },

    onAccountsRequested() {
      dispatch(Actions.AccountsRequested());
    },

    onTagTaskRequested(id) {
      dispatch(Actions.TagTaskRequested.create({ id }));
    }

  })
)(TagTaskEditorForm);

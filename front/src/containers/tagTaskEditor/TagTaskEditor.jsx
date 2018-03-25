
import React from 'react';
import { Field } from 'redux-form';
import { Button } from 'controls';
import { required } from 'form-validators';

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

export default class TagTaskEditor extends React.Component {

  constructor(props) {
    super(props);
    this.onSave = this.onSave.bind(this);
    this.onAvatarExist = this.onAvatarExist.bind(this);

    this.state = {
      avatarExistDisabled: props.avatarExistDisabled
    };
  }

  onAvatarExist(avatarExistDisabled) {
    this.setState({ avatarExistDisabled });
  }

  onSave() {
    const { avatarExistDisabled } = this.state;
    const { lastPost, followers, followings, posts, tagsInput, account } = this.props.formValues;
    const task = {
      lastPost,
      followers,
      followings,
      posts,
      avatarExistDisabled,
      tags: tagsInput.tags,
      accountId: account.id,
    };
    if (this.props.isNewTask) {
      this.props.onCreateTagTask(task);
    } else {
      this.props.onUpdateTagTask(this.props.taskId, task);
    }
  }

  loadTask() {
    if (!this.isTaskLoading) {
      this.isTaskLoading = true;
      this.props.onTagTaskRequested(this.props.taskId);
    }
  }

  loadAccounts() {
    if (!this.isAccountsLoading) {
      this.isAccountsLoading = true;
      this.props.onAccountsRequested();
    }
  }

  render() {
    const props = this.props;

    if (props.isDataLoading) {
      this.loadAccounts();
      if (!props.isNewTask) {
        this.loadTask();
      }
      return <p>waiting...</p>;
    }

    this.isTaskLoading = false;
    this.isAccountsLoading = false;

    const formValues = props.formValues || {};
    const { submitting, handleSubmit } = props;
    const state = this.state;

    return (
      <div>
        <form onSubmit={handleSubmit(this.onSave)}>
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

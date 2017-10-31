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
  Range,
  SwitchedInputGroup,
  SwitchedLabel } from 'components';
import logger from 'logger';
// import requiredIfEnabled from './validators';


function requiredIfEnabled(enabled) {
  if (enabled) {
    // debugger;
    return value => (value ? undefined : 'fff'); // required('Заполните это поле или выключите его');
  }

  return function () {};
}

class TagTaskEditor extends React.Component {

  constructor(props) {
    super(props);
    this.onAddNewTask = this.onAddNewTask.bind(this);
    this.onAccountSelected = this.onAccountSelected.bind(this);
    this.onPostsChange = this.onPostsChange.bind(this);
    this.onFollowersChange = this.onFollowersChange.bind(this);
    this.onFollowingsChange = this.onFollowingsChange.bind(this);
    this.onLastPostChange = this.onLastPostChange.bind(this);
    this.onAvatarExist = this.onAvatarExist.bind(this);

    function range(from, to, disabled) {
      return { from, to, disabled };
    }

    this.state = {
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
    this.props.change('postsFrom', posts.from);
    this.props.change('postsTo', posts.to);
    this.setState({ posts });
  }

  onFollowersChange(followers) {
    this.setState({ followers });
  }

  onFollowingsChange(followings) {
    this.setState({ followings });
  }

  onLastPostChange(lastPost) {
    this.props.change('lastPost', lastPost.value);
    this.setState({ lastPost });
  }

  onAvatarExist(avatarExist) {
    this.setState({ avatarExist });
  }

  onAddNewTask() {
    if (this.props.valid) {
      const tags = this.props.tags.map(tag => tag.tag);
      this.props.onAddNewTask({ tags, accountId: this.selectedAccount.id });
    } else {
      logger.debug('form invalid');
    }
  }


  onAccountSelected(selectedAccount) {
    this.selectedAccount = selectedAccount;
  }

  render() {
    const props = this.props;
    const validationErrors = props.validationErrors;
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
              onSelect={this.onAccountSelected}
              accounts={props.accounts}
              title="Выберите Аккаунт"
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

              <SwitchedInputGroup
                onChange={this.onLastPostChange}
                model={state.lastPost}
                label="Последняя публикация была"
                inputLabel="дня назад"
                errorMessage={validationErrors.lastPost}
              />

              <Range
                onChange={this.onPostsChange}
                model={state.posts}
                label="Количество публикаций пользователя"
              />
              <Range
                onChange={this.onFollowersChange}
                model={state.followers}
                label="Количество подписчиков пользователя"
              />
              <Range
                onChange={this.onFollowingsChange}
                model={state.followings}
                label="Количество подписок"
              />

              {/* form validation */}
              <Field
                name="lastPost"
                component="input"
                type="text"
                validate={[requiredIfEnabled(!state.lastPost.disabled)]}
              />
              <Field name="postsFrom" component="input" type="text" />
              <Field name="postsTo" component="input" type="text" />

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
  const form = state.form[formName];
  const validationErrors = form && form.syncErrors ? form.syncErrors : {};
  debugger;
  return {
    tags: state.likeTask.tags || [],
    accounts: state.account || [],
    validationErrors
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

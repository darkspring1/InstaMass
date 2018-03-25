
import { connect } from 'react-redux';
import * as Actions from 'actions';
import * as Routes from 'constants/routes';
import { reduxForm, getFormValues } from 'redux-form';
import TagTaskEditor from './TagTaskEditor';

function range(from, to, disabled) {
  return { from, to, disabled };
}

const formName = 'TagTaskEditorForm';

const TagTaskEditorForm = reduxForm({
  form: formName
})(TagTaskEditor);

function createInitialValues(tagTask) {
  if (tagTask) {
    return {
      tagsInput: { tags: tagTask.tags, value: '' },
      posts: tagTask.post,
      followers: tagTask.followers,
      followings: tagTask.followings,
      account: { id: tagTask.accountId },
      lastPost: tagTask.lastPost
    };
  }
  return {
    tagsInput: { tags: [], value: '' },
    posts: range(0, 100, false),
    followers: range(0, 100, true),
    followings: range(0, 100, true),
    account: null,
    lastPost: { value: 0, disabled: false },
  };
}


function mapStateToPropsForExistTask(state, ownProps) {
  const isDataLoading = !state.account || !state.tagTask;
  if (isDataLoading) {
    return {
      taskId: ownProps.match.params.id,
      isDataLoading,
      isNewTask: false
    };
  }

  return {
    taskId: ownProps.match.params.id,
    isNewTask: false,
    accounts: state.account || [],
    avatarExistDisabled: false,
    initialValues: createInitialValues(state.tagTask),
    formValues: getFormValues(formName)(state)
  };
}


function mapStateToPropsForNewTask(state, ownProps) {
  const isDataLoading = !state.account;
  if (isDataLoading) {
    return {
      taskId: ownProps.match.params.id,
      isDataLoading,
      isNewTask: true
    };
  }

  return {
    taskId: ownProps.match.params.id,
    isNewTask: true,
    accounts: state.account || [],
    avatarExistDisabled: false,
    initialValues: createInitialValues(),
    formValues: getFormValues(formName)(state)
  };
}


function mapStateToProps(state, ownProps) {
  const isNewTask = ownProps.match.params.id === Routes.NEW_ITEM_ID;
  if (isNewTask) {
    return mapStateToPropsForNewTask(state, ownProps);
  }
  return mapStateToPropsForExistTask(state, ownProps);
}

export default connect(
  mapStateToProps, // map state to props
  dispatch => ({
    onAddNewTag(tag) {
      dispatch(Actions.AddNewTagRequested(tag));
    },

    onCreateTagTask(task) {
      dispatch(Actions.TagTaskCreateRequest(task));
    },

    onUpdateTagTask(taskId, task) {
      debugger;
      dispatch(Actions.TagTaskUpdateRequest({ taskId, task }));
    },

    onAccountsRequested() {
      dispatch(Actions.AccountsRequested());
    },

    onTagTaskRequested(id) {
      dispatch(Actions.TagTaskGetRequest({ id }));
    }

  })
)(TagTaskEditorForm);

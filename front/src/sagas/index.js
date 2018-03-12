import AccountListSagas from 'containers/accountList/sagas';
import TaskListSagas from 'containers/taskList/sagas';
import AddNewAccount from './addNewAccount';
import AddNewTag from './addNewTag';

import AddNewLikeTask from './addNewTagTask';
import * as Auth from './auth';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    AddNewTag(),
    AccountListSagas(),
    TaskListSagas(),
    AddNewLikeTask(),
    Auth.SignIn(),
    Auth.SignInExternal(),
    Auth.SignUp(),
    Auth.SignUpExternal()
  ];
}

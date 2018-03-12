import AccountListSagas from 'containers/accountList/sagas';
import GetTasks from 'containers/taskList/sagas';
import AddNewAccount from './addNewAccount';
import AddNewTag from './addNewTag';

import AddNewLikeTask from './addNewTagTask';
import * as Auth from './auth';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    AddNewTag(),
    AccountListSagas(),
    GetTasks(),
    AddNewLikeTask(),
    Auth.SignIn(),
    Auth.SignInExternal(),
    Auth.SignUp(),
    Auth.SignUpExternal()
  ];
}

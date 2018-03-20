import AccountListSagas from 'containers/accountList/sagas';
import TaskListSagas from 'containers/taskList/sagas';
import { CreateTagTaskSaga, UpdateTagTaskSaga, GetTagTaskSaga } from 'containers/tagTaskEditor/sagas';
import AddNewAccount from './addNewAccount';
import AddNewTag from './addNewTag';

import * as Auth from './auth';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    AddNewTag(),
    AccountListSagas(),
    TaskListSagas(),
    CreateTagTaskSaga(),
    UpdateTagTaskSaga(),
    GetTagTaskSaga(),
    Auth.SignIn(),
    Auth.SignInExternal(),
    Auth.SignUp(),
    Auth.SignUpExternal()
  ];
}

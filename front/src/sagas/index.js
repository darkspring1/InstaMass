import AddNewAccount from './addNewAccount';
import AddNewTag from './addNewTag';
import GetAccounts from './getAccounts';
import GetTasks from './getTasks';
import AddNewLikeTask from './addNewTagTask';
import * as Auth from './auth';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    AddNewTag(),
    GetAccounts(),
    GetTasks(),
    AddNewLikeTask(),
    Auth.SignIn(),
    Auth.SignInExternal(),
    Auth.SignUp(),
    Auth.SignUpExternal()
  ];
}

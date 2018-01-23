import AddNewAccount from './addNewAccount';
import AddNewTag from './addNewTag';
import GetAccounts from './getAccounts';
import AddNewLikeTask from './addNewTagTask';
import * as Auth from './auth';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    AddNewTag(),
    GetAccounts(),
    AddNewLikeTask(),
    Auth.SignIn(),
    Auth.SignInExternal(),
    Auth.SignUp(),
    Auth.SignUpExternal()
  ];
}

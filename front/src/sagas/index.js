import AddNewAccount from './addNewAccount';
import AddNewTag from './addNewTag';
import GetAccounts from './getAccounts';
import AddNewLikeTask from './addNewLikeTask';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    AddNewTag(),
    GetAccounts(),
    AddNewLikeTask()
  ];
}

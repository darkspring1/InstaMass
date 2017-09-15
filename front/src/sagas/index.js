import AddNewAccount from './addNewAccount';
import AddNewTag from './addNewTag';
import GetAccounts from './getAccounts';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    AddNewTag(),
    GetAccounts()
  ];
}

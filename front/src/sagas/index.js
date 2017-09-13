import AddNewAccount from './addNewAccount';
import GetAccounts from './getAccounts';

export default function* rootSaga() {
  yield [
    AddNewAccount(),
    GetAccounts()
  ];
}

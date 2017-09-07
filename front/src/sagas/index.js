import addNewAccount from './addNewAccount';

export default function* rootSaga() {
  yield [
    addNewAccount()
  ];
}

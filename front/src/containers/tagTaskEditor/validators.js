// import { required } from 'form-validators';


export default function (enabled) {
  if (enabled) {
    // debugger;
    return value => (value ? undefined : 'fff'); // required('Заполните это поле или выключите его');
  }

  return function () {};
}

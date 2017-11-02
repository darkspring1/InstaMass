import { required } from 'form-validators';

export default function (message) {
  const requeredFunc = required(message);
  return (model) => {
    if (!model || model.disabled) {
      return undefined;
    }
    return requeredFunc(model.value);
  };
}

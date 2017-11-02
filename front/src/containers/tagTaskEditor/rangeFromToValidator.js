import { isEmpty } from 'form-validators';

export default function (message) {
  return (model) => {
    if (!model || model.disabled) {
      return undefined;
    }

    if (isEmpty(model.from) || isEmpty(model.from)) {
      return undefined;
    }

    const fromInt = parseInt(model.from, 10);
    const toInt = parseInt(model.to, 10);

    if (fromInt < toInt) {
      return undefined;
    }

    return { from: message, to: ' ' };
  };
}

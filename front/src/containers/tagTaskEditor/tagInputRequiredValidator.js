import { isEmptyArray } from 'form-validators';

export default function (message) {
  return (model) => {
    if (!model) {
      return message;
    }

    if (model.value) {
      return undefined;
    }

    if (isEmptyArray(model.tags)) {
      return message;
    }

    return undefined;
  };
}

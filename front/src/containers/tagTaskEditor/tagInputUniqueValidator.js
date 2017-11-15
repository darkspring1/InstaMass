import { isEmptyArray } from 'form-validators';

const tagName = '{tag_name}';

export default function (message) {
  return (model) => {
    if (model && model.value && !isEmptyArray(model.tags) && model.tags.includes(model.value)) {
      return message.replace(tagName, model.value);
    }

    return undefined;
  };
}

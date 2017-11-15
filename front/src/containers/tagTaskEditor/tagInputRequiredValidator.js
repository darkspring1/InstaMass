/* eslint no-unused-vars: 0 */

function isEmptyArray(value) {
  return value == null || value === undefined || value.length === 0;
}


export default function (message) {
  return (model, allValues, props, name) => {
    if (!model || isEmptyArray(model.tags)) {
      return message;
    }

    return undefined;
  };
}

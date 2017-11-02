

function isEmpty(value) {
  return value == null || value === undefined || value === '';
}

function required(message) {
  return value => (isEmpty(value) ? message : undefined);
}


function maxLength(max, message) {
  return value => (value && value.length > max ? message : undefined);
}


export { required };
export { maxLength };
export { isEmpty };

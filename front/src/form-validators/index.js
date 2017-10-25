

function required(message) {
  return value => (value ? undefined : message);
}


function maxLength(max, message) {
  return value => (value && value.length > max ? message : undefined);
}


export { required };
export { maxLength };

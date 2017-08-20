
window.common = (function cmn() {
  const common = {};

  function parseQueryString(queryString) {
    const data = {};

    let pair;
    let separatorIndex;
    let escapedKey;
    let escapedValue;
    let key;
    let value;

    if (queryString === null) {
      return data;
    }

    const pairs = queryString.split('&');

    for (let i = 0; i < pairs.length; i += 1) {
      pair = pairs[i];
      separatorIndex = pair.indexOf('=');

      if (separatorIndex === -1) {
        escapedKey = pair;
        escapedValue = null;
      } else {
        escapedKey = pair.substr(0, separatorIndex);
        escapedValue = pair.substr(separatorIndex + 1);
      }

      key = decodeURIComponent(escapedKey);
      value = decodeURIComponent(escapedValue);

      data[key] = value;
    }

    return data;
  }

  common.getFragment = function getFragment() {
    if (window.location.hash.indexOf('#') === 0) {
      return parseQueryString(window.location.hash.substr(1));
    }
    return {};
  };

  return common;
}());

debugger;
const fragment = window.common.getFragment();

window.location.hash = fragment.state || '';

window.opener.$windowScope.authCompletedCB(fragment);

window.close();


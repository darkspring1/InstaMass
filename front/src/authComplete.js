
function extractAuthInf() {
  debugger;
  const search = decodeURIComponent(window.location.search);
  const start = search.indexOf('{');
  const finish = search.indexOf('}');
  const authInf = search.substring(start, finish);
  debugger;
  return JSON.parse(authInf);
}

debugger;
const authInf = extractAuthInf();


window.opener.dispatchAuthExternalProvider(authInf);

window.close();


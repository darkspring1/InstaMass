function extractAuthInf() {
  const search = decodeURIComponent(window.location.search);
  const start = search.indexOf('{');
  const finish = search.indexOf('}');
  const authInf = search.substring(start, finish + 1);
  return JSON.parse(authInf);
}

const authInf = extractAuthInf();

window.opener.dispatchAuthExternalProvider(authInf);

window.close();

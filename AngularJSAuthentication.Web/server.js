const express = require('express');
const httpProxy = require('http-proxy');
const path = require('path');

const app = express();

const port = 666;
const host = `localhost:${port}`;
const proxyTarget = 'moskva.stage.beeline.ru';
const target = { target: `http://${proxyTarget}:80` };
//const proxy = httpProxy.createProxyServer();

process.on('uncaughtException', (err) => {
    // handle the error safely
  console.log(err);
});

function replaceInHeader(req, header) {
  if (req.headers[header]) {
    const newHeader = req.headers[header].replace(host, proxyTarget);
    req.headers[header] = newHeader;
  }
}

function requestProxy(req, res) {
  replaceInHeader(req, 'host');
  replaceInHeader(req, 'origin');
  replaceInHeader(req, 'referer');
  proxy.web(req, res, target);
}

app.use(express.static(path.resolve(__dirname, './')));

/*
app.get('/*', requestProxy);

app.post('/*', requestProxy);
*/

app.listen(port);

console.log(`App listening on port ${port}`);

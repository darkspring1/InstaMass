const express = require('express');
const httpProxy = require('http-proxy');

const app = express();
const path = require('path');

const port = 8080;

const apiProxy = httpProxy.createProxyServer();

const host = `localhost:${port}`;

//const proxyTarget = 'moskva.stage.beeline.ru';

//const target = { target: `http://${proxyTarget}:80` };


process.on('uncaughtException', (err) => {
    // handle the error safely
  console.log(err);
});

/*
function replaceInHeader(req, header) {
  if (req.headers[header]) {
    req.headers[header] = req.headers[header].replace(host, proxyTarget);
  }
}

function requestProxy(req, res) {
// console.log(req.url);
  replaceInHeader(req, 'host');
  replaceInHeader(req, 'origin');
  replaceInHeader(req, 'referer');
  apiProxy.web(req, res, target);
}
*/
app.use(express.static(`${__dirname}/`));
app.use(express.static(`${__dirname}/dist`));
/*
app.get('/abtest/inlinescript*', (req, res) => {
  console.log(req.url);

  if (req.url.indexOf('test-1176293=1') !== -1) {
    console.log('1.build.js');
    res.sendFile(path.join(`${__dirname}/dist/a.bundle.js`));
  } else {
    res.status(404).send('Not found');
  }
});


app.post('/*', requestProxy);

app.get('/*', requestProxy);
*/

app.listen(port);

console.log(`App listening on port ${port}`);

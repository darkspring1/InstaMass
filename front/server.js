const express = require('express');
const httpProxy = require('http-proxy');

const app = express();
const path = require('path');

const port = 8080;

const apiProxy = httpProxy.createProxyServer();

const host = `localhost:${port}`;


process.on('uncaughtException', (err) => {
    // handle the error safely
  console.log(err);
});


app.use(express.static(`${__dirname}/`));
app.use(express.static(`${__dirname}/dist`));
app.use(express.static(`${__dirname}/src`));

const fileExtensions = ['woff', 'woff2', 'ttf', 'js', 'css', 'png'];

function isFile(url) {
  for (const i in fileExtensions) {
    if (url.indexOf(`.${fileExtensions[i]}`) != -1) {
      return true;
    }
  }
  return false;
}

function appGet(url) {
  app.get(`${url}*`, (req, res) => {
        // console.log(req.url);
    if (isFile(req.url)) {
      console.log(`${404} ${req.url}`);
      res.status(404).send('Not found');
      return;
    }
    res.sendFile(path.join(`${__dirname + url}/index.html`));
  });
}

appGet('/');

app.listen(port);

console.log(`App listening on port ${port}`);

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


app.get('/*', (req, res) => {
  res.sendFile(path.join(`${__dirname}/index.html`));
});

app.listen(port);

console.log(`App listening on port ${port}`);

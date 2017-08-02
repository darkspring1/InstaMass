var express = require('express');
var httpProxy = require('http-proxy');
var app = express();
var path = require('path');

var port = 8080;

var apiProxy = httpProxy.createProxyServer();
var apiPort = 90;

process.on('uncaughtException', function(err) {
    // handle the error safely
    console.log(err)
})

app.use(express.static(__dirname + '/src'));  

app.post("/api*", function(req, res){
	console.log(req.url);
  apiProxy.web(req, res, { target: 'http://localhost:' + apiPort });
});

app.get("/api*", function(req, res){
	console.log(req.url);
	apiProxy.web(req, res, { target: 'http://localhost:' + apiPort });
});

app.get('/*', function(req, res) {
    res.sendFile(path.join(__dirname + '/index.html'));
});


app.listen(port);

console.log("App listening on port " + port);
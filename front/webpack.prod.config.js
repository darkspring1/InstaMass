/* eslint-disable */

const NODE_ENV = process.env.NODE_ENV || 'production';
console.log('NODE_ENV=' + NODE_ENV);

const webpack = require('webpack');
const path = require('path');
const WebpackNotifierPlugin = require('webpack-notifier');

module.exports = {
  entry: {
     a: [
      './src/AbTest.js',
    ],
  },
  output: {
    path: path.resolve(__dirname, 'dist'),
   filename: '[name].bundle.js',
  },
  module: {
    rules: [
      {
        enforce: 'pre',
        test: /\.(js|jsx)?$/,
        exclude: /node_modules/,
        loader: 'eslint-loader',
        options: {
          failOnError: true
        }
      }, {
        test: /\.(js|jsx)$/,
        use: 'babel-loader',
        exclude: /node_modules/
      }, {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      }, {
        test: /\.(png|gif|woff|woff2|eot|ttf|svg)$/,
        loader: 'url-loader?limit=100000'
      }
    ]
  },
  resolve: {
    extensions: ['.js', '.jsx', '.json']
  },
  plugins: [
    new WebpackNotifierPlugin(),
   new webpack.DefinePlugin({ NODE_ENV: JSON.stringify(NODE_ENV) }),
    new webpack.optimize.UglifyJsPlugin()
  ]
};

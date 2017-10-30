/* eslint-disable */
const NODE_ENV = process.env.NODE_ENV || 'development';
console.log('NODE_ENV=' + NODE_ENV);

const webpack = require('webpack');
const path = require('path');
const WebpackNotifierPlugin = require('webpack-notifier');

module.exports = {
  devtool: 'source-map',
  entry: {
    main: [
      'babel-polyfill',
      './src/index.js',
    ],
    authComplete : [
      './src/authComplete.js',
    ]
  },
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: '[name].bundle.js',
  },
  devtool: 'inline-source-map',
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
      },
      {
      test: /\.(js|jsx)?$/,
      use: 'babel-loader',
      exclude: /node_modules/,
    }, {
      test: /\.css$/,
      use: ['style-loader', 'css-loader'],
    }, {
      test: /\.(png|gif|woff|woff2|eot|ttf|svg|jpg)?$/,
      loader: 'url-loader?limit=100000',
    }],
  },
  resolve: {
    extensions: ['.js', '.jsx', '.json'],
    modules: ['src', 'node_modules'],
    mainFiles: ['index']
  },
  plugins: [
    // new WebpackNotifierPlugin(),
    new webpack.DefinePlugin({ NODE_ENV: JSON.stringify(NODE_ENV) }),
    // new webpack.HotModuleReplacementPlugin(),
    new webpack.NamedModulesPlugin(),
  ],
  // devServer: {
  //   hot: true
  // },
  watch: true,

  node: {
    fs: "empty"
 }
};

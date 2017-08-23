import React from 'react';
import { Orders } from './api';

export default () => {
  Orders();
  return (
    <div className="content-top clearfix">
      <h1 className="al-title ng-binding">Dashboard</h1>

      <ul className="breadcrumb al-breadcrumb">
        <li>
          <a href="#/dashboard">Home</a></li>
        <li className="ng-binding">Dashboard</li>
      </ul>
    </div>);
};

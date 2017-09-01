import React from 'react';

export default props => (
  <div className="content-top clearfix">
    <h1 className="al-title">{props.title}</h1>

    <ul className="breadcrumb al-breadcrumb">
      <li>
        <a href="#/dashboard">Home</a></li>
      <li>Dashboard</li>
    </ul>
  </div>);

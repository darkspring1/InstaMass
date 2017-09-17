import React from 'react';

export default props => (<div
  className="panel popular-app"
  style={{ backgroundSize: '1913px 1076px', backgroundPosition: '0px -197px' }}
>
  <div className="panel-body">
    <div className="popular-app-img-container">
      <div className="popular-app-img">
        {/* <img alt="logo" style={{ 'border-radius': '100px' }} src="https://scontent-frx5-1.cdninstagram.com/t51.2885-19/s150x150/18644749_231907577304330_2681218082822684672_a.jpg" />
        <span className="logo-text">darkspringdarkspring</span> */}

        <img alt="logo" src="assets/img/app/my-app-logo.png" />
        <span className="logo-text">{props.account.login}</span>
      </div>
    </div>
    <div className="popular-app-cost row">
      <div className="col-xs-9">Likes</div>
      <div className="col-xs-3 text-right">175$</div>
    </div>

    <div className="popular-app-cost row">
      <div className="col-xs-9">Likes</div>
      <div className="col-xs-3 text-right">175$</div>
    </div>

    <div className="popular-app-info row">
      <div className="col-xs-4 text-left"><div className="info-label">Total Visits</div><div>47,512</div>
      </div>
      <div className="col-xs-4 text-center"><div className="info-label">New Visits</div>
        <div>9,217</div></div>
      <div className="col-xs-4 text-right"><div className="info-label">Sales</div>
        <div>2,928</div>
      </div>
    </div>

  </div>
</div>);

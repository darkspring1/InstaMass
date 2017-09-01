/* eslint jsx-a11y/label-has-for: 0 */
/* eslint react/prefer-stateless-function: 0 */
/* eslint no-useless-constructor: 0 */
/* eslint : 0 */

import React from 'react';
// import { Orders } from './api';

import ContentTop from './contentTop';

export default class Dashboard extends React.Component {

  constructor(props) {
    super(props);
  }


  // Orders();

  render() {
    return (
      <div>
        <ContentTop title="Account" />

        <div
          className="panel panel-blur profile-page animated zoomIn"
          style={{ backgroundSize: '1359px 764px', backgroundPosition: '0px -224px' }}
        >
          <div className="panel-body" >
            <div className="panel-content ng-scope">
              <h3 className="with-line">General Information</h3>
            </div>
          </div>
        </div>
      </div>


    );
  }

}

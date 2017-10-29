/* eslint jsx-a11y/label-has-for: 0 */
import React from 'react';
import { InputGroup } from '../controls/';

export default props => (
  <div className="row">
    <div className="col-md-6" >
      <div className="form-group">
        <label>{props.label}</label>
        <InputGroup value={props.from} primary left="от" />
      </div>
    </div>

    <div className="col-md-6" >
      <div className="form-group">
        <label>&nbsp;</label>
        <InputGroup value={props.to} primary left="до" />
      </div>
    </div>
  </div>
  );

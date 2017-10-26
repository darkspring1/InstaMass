/* eslint jsx-a11y/label-has-for: 0 */
import React from 'react';

export default props => (

  <div className="row">

    <div className="col-md-6" >

      <div className="form-group">
        <label>Количество публикаций пользователя</label>
        <div className="input-group">
          <span className="input-group-addon input-group-addon-primary addon-left">от</span>
          <input
            value={props.from}
            type="text"
            className="form-control with-primary-addon"
          />
        </div>

      </div>

    </div>

    <div className="col-md-6" >
      <div className="form-group">
        <label>&nbsp;</label>
        <div className="input-group">
          <span className="input-group-addon input-group-addon-primary addon-left">до</span>
          <input
            value={props.to}
            type="text"
            className="form-control with-primary-addon"
          />
        </div>
      </div>
    </div>

  </div>

);

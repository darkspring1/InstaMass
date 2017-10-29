import React from 'react';

import classBuilder from './utils';

function inputGroupAddonBuilder(str) {
  return `input-group-addon-${str}`;
}

export default (props) => {
  const spanClass = `input-group-addon ${classBuilder(props, inputGroupAddonBuilder)}`;

  function renderSpan(text, cssClass, subClass) {
    if (text) {
      return <span className={cssClass + subClass}>{text}</span>;
    }
    return null;
  }


  return (

    <div className="input-group">
      {renderSpan(props.left, spanClass, ' addon-left')}
      <input
        value={props.value}
        type="text"
        className="form-control with-primary-addon"
      />
      {renderSpan(props.right, spanClass, ' addon-right')}
    </div>
  );
};

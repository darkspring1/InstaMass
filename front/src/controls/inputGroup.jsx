import React from 'react';

import classBuilder from './utils';

function inputGroupAddonBuilder(str) {
  return `input-group-addon-${str}`;
}

export default (props) => {
  const spanClass = `input-group-addon ${classBuilder(props, inputGroupAddonBuilder)}`;
  let inputGroupClass = 'input-group';

  if (props.hasError) {
    inputGroupClass += ' has-error';
  }

  function renderSpan(text, cssClass, subClass) {
    if (text) {
      return <span className={cssClass + subClass}>{text}</span>;
    }
    return null;
  }

  function onChange(event) {
    props.onChange(event.target.value, event);
  }

  return (

    <div className={inputGroupClass}>
      {renderSpan(props.left, spanClass, ' addon-left')}
      <input
        value={props.value}
        type="text"
        onChange={onChange}
        disabled={props.disabled}
        className="form-control with-primary-addon"
      />
      {renderSpan(props.right, spanClass, ' addon-right')}
    </div>
  );
};

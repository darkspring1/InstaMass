/* eslint jsx-a11y/label-has-for:0 */
import React from 'react';

export default ({
  input,
  label,
  type,
  meta: { touched, error /* , warning */ }
}) => {
  const hasError = touched && error;

  let cssClass = 'form-group';

  if (hasError) {
    cssClass += ' has-error';
  }

  function renderError() {
    if (hasError) {
      return <span className="text-right help-block">{error}</span>;
    }
    return null;
  }

  return (<div className={cssClass}>
    <label className="col-sm-2 control-label">{label}</label>

    <div className="col-sm-10">
      <input
        {...input}
        type={type}
        className="form-control"
        placeholder={label}
      />
      { renderError() }
    </div>
  </div>);
};

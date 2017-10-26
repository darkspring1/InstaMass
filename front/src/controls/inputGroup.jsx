import React from 'react';

export default (props) => {
  let btnClassName = `btn ${props.cssClass}`;
  if (props.primary) {
    btnClassName += ' btn-primary';
  } else if (props.danger) {
    btnClassName += ' btn-danger';
  } else if (props.success) {
    btnClassName += ' btn-success';
  } else {
    btnClassName += ' btn-default';
  }

  if (props.large) {
    btnClassName += ' btn-lg';
  }

  let icon = null;
  if (props.icon) {
    btnClassName += ' btn-with-icon';
    icon = <i className={props.icon} />;
  }

  return (
    <button type="button" onClick={props.onClick} className={btnClassName} disabled={props.disabled}>
      {icon}{props.text}
    </button>);
};

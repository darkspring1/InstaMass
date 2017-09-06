import React from 'react';

export default (props) => {
  let btnClassName = `btn ${props.cssClass}`;
  if (props.primary) {
    btnClassName += ' btn-primary';
  } else {
    btnClassName += ' btn-default';
  }

  let icon = null;
  if (props.icon) {
    btnClassName += ' btn-with-icon';
    icon = <i className={props.icon} />;
  }

  return (
    <button type="button" onClick={props.onClick} className={btnClassName}>
      {icon}{props.text}
    </button>);
};

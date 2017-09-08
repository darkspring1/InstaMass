import React from 'react';

export default (props) => {
  const style = {};
  const spinnerStyle = {};
  if (!props.fullscreen) {
    style.top = 'auto';
    style.left = 'auto';
    style.opacity = '0.5';

    spinnerStyle.margin = '-144px 0 0 -144px';
  }


  return (
    <div style={style} className="preloader" >
      <div style={spinnerStyle} />
    </div>
  );
};

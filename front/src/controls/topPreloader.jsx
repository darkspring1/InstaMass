import React from 'react';

export default (props) => {
  const style = {};
  if (!props.visible) {
    style.display = 'none';
  }

  return (
    <div style={style} className="top-preloader">
      <div className="alert bg-info">
        <h4>Загрузка...</h4>
      </div>
    </div>);
}
;

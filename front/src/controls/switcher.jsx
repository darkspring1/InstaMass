/* eslint jsx-a11y/no-static-element-interactions: 0 */

import React from 'react';
import classBuilder from './utils';

function switcherBuilder(str) {
  return `switcher ${str}`;
}


export default (props) => {
  const switcherClass = classBuilder(props, switcherBuilder);
  return (
    <div
      className="switcher-container"
      onClick={props.onChange}
    >
      <input type="checkbox" checked={!!props.checked} />
      <div className={switcherClass} >
        <div className="handle-container">
          <span className="handle handle-on">{props['on-text']}</span>
          <span className="handle" />
          <span className="handle handle-off">{props['off-text']}</span>
        </div>
      </div>
    </div>
  );
};

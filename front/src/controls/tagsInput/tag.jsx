
/* eslint jsx-a11y/no-static-element-interactions: 0 */

import React from 'react';

export default props => (
  <span className="tag label label-primary">
    {props.text}
    <span
      data-role="remove"
      onClick={() => props.onRemoveTag(props.text)}
    />
  </span>);

/* eslint-disable */

import React from 'react';
import { TagsInput } from 'components';

const RenderTagsInput = ({
  input,
  meta: { touched, error /* , warning */ },
  model,
  placeholder
}) => {
  return (<div>
    <input {...input} type="hidden" />
    <TagsInput
      placeholder={placeholder}
      model={model}
      onChange={input.onChange}
      onBlur={input.onBlur}
      onFocus={input.onFocus}
      errorMessage={ touched && error ? error : '' }
    />
  </div>);
};


export default RenderTagsInput;

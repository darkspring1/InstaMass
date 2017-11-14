/* eslint-disable */

import React from 'react';
import { TagsInput } from 'components';

const RenderTagsInput = ({
  input,
  meta: { /* touched, */error/* , warning */ },
  model,
  placeholder
}) => {
  //debugger;
  return (<div>
    <input {...input} type="hidden" />

    <TagsInput
      placeholder={placeholder}
      model={model}
      onChange={input.onChange}
      hasError={!!error}
    />
  </div>);
};


export default RenderTagsInput;

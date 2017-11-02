import React from 'react';
import { SwitchedInputGroup } from 'components';

const RenderSwitchedInputGroupField = ({
  input,
  label,
  meta: { /* touched, */error/* , warning */ },
  model,
  inputLabel
}) => (
  <div>
    <input {...input} type="hidden" />
    <SwitchedInputGroup
      onChange={input.onChange}
      model={model}
      label={label}
      inputLabel={inputLabel}
      errorMessage={error}
    />
  </div>);

export default RenderSwitchedInputGroupField;

import React from 'react';
import { SwitchedInputGroup } from 'components';

const RenderSwitchedInputGroupField = ({
  input,
  label,
  meta: { /* touched, */error/* , warning */ },
  inputLabel
}) => (
  <div>
    <input {...input} type="hidden" />
    <SwitchedInputGroup
      onChange={input.onChange}
      model={input.value}
      label={label}
      inputLabel={inputLabel}
      errorMessage={error}
    />
  </div>);

export default RenderSwitchedInputGroupField;

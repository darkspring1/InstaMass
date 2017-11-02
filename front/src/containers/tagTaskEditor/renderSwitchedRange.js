import React from 'react';
import { SwitchedRange } from 'components';

const RenderSwitchedInputGroupField = ({
  input,
  label,
  meta: { /* touched, */error/* , warning */ },
  model
}) => {
  const errorFrom = error ? error.from : null;
  const errorTo = error ? error.to : null;
  return (
    <div>
      <input {...input} type="hidden" />

      <SwitchedRange
        onChange={input.onChange}
        model={model}
        label={label}
        fromErrorMessage={errorFrom}
        toErrorMessage={errorTo}
      />
    </div>);
};

export default RenderSwitchedInputGroupField;

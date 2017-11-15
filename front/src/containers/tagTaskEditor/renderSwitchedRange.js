import React from 'react';
import { SwitchedRange } from 'components';

const RenderSwitchedInputGroupField = ({
  input,
  label,
  meta: { touched, error },
}) => {
  const errorFrom = touched && error ? error.from : null;
  const errorTo = touched && error ? error.to : null;
  return (
    <div>
      <input {...input} type="hidden" />

      <SwitchedRange
        onChange={input.onChange}
        onBlur={input.onBlur}
        onFocus={input.onFocus}
        model={input.value}
        label={label}
        fromErrorMessage={errorFrom}
        toErrorMessage={errorTo}
      />
    </div>);
};

export default RenderSwitchedInputGroupField;

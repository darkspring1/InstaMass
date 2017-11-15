import React from 'react';
import { TagsInput } from 'components';

const RenderTagsInput = ({
  input,
  meta: { touched, error },
  placeholder
}) => (<div>
  <TagsInput
    placeholder={placeholder}
    model={input.value}
    onChange={input.onChange}
    onBlur={input.onBlur}
    onFocus={input.onFocus}
    errorMessage={touched && error ? error : ''}
  />
</div>);


export default RenderTagsInput;

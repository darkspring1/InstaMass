import React from 'react';
import { TagsInput } from 'components';

const RenderTagsInput = ({
  input,
  meta: { /* touched, */error/* , warning */ },
  placeholder,
  onAddTag,
  tags,
  onRemoveTag
}) => {
  debugger;
  return (<div>
    <input {...input} type="hidden" />

    <TagsInput
      placeholder={placeholder}
      onAddTag={onAddTag}
      value={input.value}
      onChange={input.onChange}
      onRemoveTag={onRemoveTag}
      tags={tags}
      hasError={!!error}
    />
  </div>);
};


export default RenderTagsInput;

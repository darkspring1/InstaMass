import React from 'react';

import { TagsInput, Tag } from 'controls';

export default (props) => {
  debugger;
  const tags = props.tags.map(tag => <Tag text={tag} key={tag} onRemoveTag={props.onRemoveTag} />);
  return (<TagsInput
    placeholder={props.placeholder}
    onChange={props.onChange}
    onAddTag={props.onAddTag}
    value={props.value}
  >
    {tags}
  </TagsInput>
  );
}
;

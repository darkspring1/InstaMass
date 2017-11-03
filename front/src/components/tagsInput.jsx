import React from 'react';

import { TagsInput, Tag } from 'controls';

export default (props) => {
  const tags = props.tags.map(tag => <Tag text={tag} key={tag} onRemoveTag={props.onRemoveTag} />);
  return (<TagsInput
    placeholder={props.placeholder}
    onChange={props.onChange}
    onAddTag={props.onAddTag}
  >
    {tags}
  </TagsInput>
  );
}
;

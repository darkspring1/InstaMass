/* eslint class-methods-use-this: 0 */
/* eslint no-unused-vars: 0 */

import React from 'react';

import { TagsInput, Tag } from 'controls';

export default class tagsInput extends React.Component {

  constructor(props) {
    super(props);
    this.onChange = this.onChange.bind(this);
    this.onRemoveTag = this.onRemoveTag.bind(this);
    this.onAddTag = this.onAddTag.bind(this);
  }

  onChange(event) {
    const value = event.target.value;
    this.props.onChange({ value, tags: this.props.model.tags }, event);
  }

  onRemoveTag(tag, event) {
    const model = this.props.model;
    const index = model.tags.indexOf(tag);
    model.tags.splice(index, 1);
    this.props.onChange({ value: model.value, tags: model.tags }, event);
  }

  onAddTag(tag, event) {
    const model = this.props.model;
    model.tags.push(tag);
    this.props.onChange({ value: '', tags: model.tags });
  }

  render() {
    const props = this.props;
    let error = null;
    let cssClass = 'form-group';
    if (props.errorMessage) {
      error = <span className="help-block">{props.errorMessage}</span>;
      cssClass += ' has-error';
    }
    const tags = props.model.tags.map(tag => <Tag text={tag} key={tag} onRemoveTag={this.onRemoveTag} />);
    return (
      <div className={cssClass}>
        <TagsInput
          hasError={!!props.errorMessage}
          placeholder={props.placeholder}
          onChange={this.onChange}
          onAddTag={this.onAddTag}
          onBlur={props.onBlur}
          onFocus={props.onFocus}
          value={props.model.value}
        >
          {tags}
        </TagsInput>
        {error}
      </div>
    );
  }

}

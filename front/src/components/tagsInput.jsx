
import React from 'react';
import { TagsInput, Tag } from 'controls';

export default class tagsInput extends React.Component {

  static clone(obj) {
    return JSON.parse(JSON.stringify(obj));
  }

  constructor(props) {
    super(props);
    this.onChange = this.onChange.bind(this);
    this.onRemoveTag = this.onRemoveTag.bind(this);
    this.onAddTag = this.onAddTag.bind(this);
    this.onBlur = this.onBlur.bind(this);
    this.onFocus = this.onFocus.bind(this);
  }

  onChange(event) {
    const model = this.cloneModel();
    model.value = event.target.value;
    this.props.onChange(model);
  }

  onRemoveTag(tag, event) {
    const model = this.cloneModel();
    const index = model.tags.indexOf(tag);
    model.tags.splice(index, 1);
    this.props.onChange(model, event);
    this.props.onBlur(model);
  }

  onAddTag(tag) {
    const model = this.cloneModel();
    if (!model.tags.includes(tag)) {
      model.tags.push(tag);
    }
    model.value = '';
    this.props.onChange(model);
  }

  onBlur() {
    this.props.onBlur(this.cloneModel());
  }

  onFocus() {
    this.props.onFocus(this.cloneModel());
  }

  cloneModel() {
    return tagsInput.clone(this.props.model);
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
          onBlur={this.onBlur}
          onFocus={this.onFocus}
          value={props.model.value}
        >
          {tags}
        </TagsInput>
        {error}
      </div>
    );
  }

}

import React from 'react';

export default class TagsInput extends React.Component {

  constructor(props) {
    super(props);
    this.onChange = this.onChange.bind(this);
    this.onAddTag = this.onAddTag.bind(this);
    this.onKeyPress = this.onKeyPress.bind(this);
  }

  onChange(event) {
    if (this.props.onChange) {
      this.props.onChange(event);
    }
  }

  onAddTag(event) {
    if (this.props.value) {
      this.props.onAddTag(this.props.value, event);
    }
  }

  onKeyPress(event) {
    if (event.key === 'Enter') {
      this.onAddTag(event);
    }
  }

  render() {
    const props = this.props;

    let style = null;
    if (props.hasError) {
      style = { border: '1px solid #ed7878' };
    }

    return (
      <div className="bootstrap-tagsinput" style={style}>
        {props.children}
        <input
          type="text"
          onChange={this.onChange}
          value={this.props.value}
          onBlur={this.onAddTag}
          onKeyPress={this.onKeyPress}
          placeholder={props.placeholder}
        />
      </div>
    );
  }

}

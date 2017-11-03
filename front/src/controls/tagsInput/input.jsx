import React from 'react';

export default class TagsInput extends React.Component {

  constructor(props) {
    super(props);
    this.onChange = this.onChange.bind(this);
    this.onAddTag = this.onAddTag.bind(this);
    this.onKeyPress = this.onKeyPress.bind(this);
    this.state = { tag: null };
  }

  onChange(event) {
    const tag = event.target.value;
    if (this.props.onChange) {
      this.props.onChange(tag, event);
    }

    this.setState({ tag });
  }

  onAddTag(event) {
    if (this.state.tag) {
      this.props.onAddTag(this.state.tag, event);
    }
    this.setState({ tag: '' });
  }

  onKeyPress(event) {
    if (event.key === 'Enter') {
      this.onAddTag(event);
    }
  }

  render() {
    const props = this.props;
    return (
      <div className="bootstrap-tagsinput">
        {props.children}
        <input
          type="text"
          onChange={this.onChange}
          value={this.state.tag}
          onBlur={this.onAddTag}
          onKeyPress={this.onKeyPress}
          placeholder={props.placeholder}
        />
      </div>
    );
  }

}

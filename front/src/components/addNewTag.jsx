import React from 'react';
import Button from '../controls/button';

export default class AddNewTag extends React.Component {

  constructor(props) {
    super(props);
    this.onChange = this.onChange.bind(this);
    this.state = { tag: '' };
  }

  onChange(event) {
    this.setState({ tag: event.target.value });
  }

  render() {
    const props = this.props;
    return (<div className="row">
      <div className="form-group col-sm-3 col-xs-6">
        <input
          type="text"
          className="form-control"
          placeholder="Добавить новый хэштег"
          onChange={this.onChange}
          value={this.state.tag}
        />
      </div>

      <div className="form-group col-sm-3 col-xs-6">
        <Button
          text="Добавить"
          disabled={!this.state.tag}
          primary
          onClick={() => props.onAddBtnClick(this.state.tag)}
        />
      </div>

    </div>);
  }

}

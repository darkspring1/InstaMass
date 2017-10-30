/* eslint jsx-a11y/label-has-for: 0 */
/* eslint class-methods-use-this: 0 */
import React from 'react';

import { InputGroup, Switcher } from 'controls/';


export default class Range extends React.Component {

  constructor(props) {
    super(props);

    this.onSwitcherChange = this.onSwitcherChange.bind(this);
    this.onFromChange = this.onFromChange.bind(this);
    this.onToChange = this.onToChange.bind(this);
  }

  onSwitcherChange() {
    const props = this.props;
    props.onChange({ from: props.from, to: props.to, disabled: !props.disabled });
  }

  onFromChange(from) {
    const props = this.props;
    props.onChange({ from, to: props.to, disabled: props.disabled });
  }

  onToChange(to) {
    const props = this.props;
    props.onChange({ from: props.from, to, disabled: props.disabled });
  }

  render() {
    const props = this.props;
    return (<div className="row">
      <div className="col-md-1">
        <div style={{ 'margin-top': '21px' }}>
          <Switcher
            checked={!props.disabled}
            primary
            on-text="вкл"
            onChange={this.onSwitcherChange}
            off-text="выкл"
          />
        </div>
      </div>
      <div className="col-md-5" >
        <div className="form-group">
          <label>{props.label}</label>
          <InputGroup disabled={props.disabled} onChange={this.onFromChange} value={props.from} primary left="от" />
        </div>
      </div>

      <div className="col-md-6" >
        <div className="form-group">
          <label>&nbsp;</label>
          <InputGroup disabled={props.disabled} onChange={this.onToChange} value={props.to} primary left="до" />
        </div>
      </div>
    </div>);
  }
}

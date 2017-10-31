/* eslint jsx-a11y/label-has-for: 0 */
/* eslint class-methods-use-this: 0 */
import React from 'react';

import { InputGroup, Switcher } from 'controls/';


export default class switchedInputGroup extends React.Component {

  constructor(props) {
    super(props);
    this.onSwitcherChange = this.onSwitcherChange.bind(this);
    this.onValueChange = this.onValueChange.bind(this);
  }

  onSwitcherChange() {
    const props = this.props;
    const model = { ...props.model };
    model.disabled = !model.disabled;
    props.onChange(model);
  }

  onValueChange(value) {
    const props = this.props;
    const model = { ...props.model, ...{ value } };
    props.onChange(model);
  }

  render() {
    const props = this.props;
    let error = null;
    if (props.errorMessage) {
      error = <span className="help-block">error</span>;
    }
    return (<div className="row">
      <div className="col-md-1">
        <div style={{ marginTop: '21px' }}>
          <Switcher
            checked={!props.model.disabled}
            primary
            on-text="вкл"
            onChange={this.onSwitcherChange}
            off-text="выкл"
          />
        </div>
      </div>
      <div className="col-md-11" >
        <div className={!error ? 'form-group' : 'form-group has-error'}>
          <label>{props.label}</label>
          <InputGroup
            disabled={props.model.disabled}
            onChange={this.onValueChange}
            value={props.model.value}
            primary
            right={props.inputLabel}
            hasError={!!error}
          />
          {error}
        </div>
      </div>

    </div>);
  }
}

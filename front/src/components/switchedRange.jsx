/* eslint jsx-a11y/label-has-for: 0 */
import React from 'react';

import { InputGroup, Switcher } from 'controls/';


export default class Range extends React.Component {

  static GetErrorMessage(errorMessage) {
    if (errorMessage) {
      return <span className="help-block">{errorMessage}</span>;
    }
    return null;
  }
  constructor(props) {
    super(props);

    this.onSwitcherChange = this.onSwitcherChange.bind(this);
    this.onFromChange = this.onFromChange.bind(this);
    this.onToChange = this.onToChange.bind(this);
  }

  onSwitcherChange() {
    const props = this.props;
    const model = { ...props.model };
    model.disabled = !model.disabled;
    props.onChange(model);
  }

  onFromChange(from) {
    const props = this.props;
    const model = { ...props.model, ...{ from } };
    props.onChange(model);
  }

  onToChange(to) {
    const props = this.props;
    const model = { ...props.model, ...{ to } };
    props.onChange(model);
  }

  render() {
    const props = this.props;
    const fromError = Range.GetErrorMessage(props.fromErrorMessage);
    const toError = Range.GetErrorMessage(props.toErrorMessage);
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
      <div className="col-md-5" >
        <div className={fromError ? 'form-group has-error' : 'form-group'}>
          <label>{props.label}</label>
          <InputGroup
            disabled={props.model.disabled}
            onChange={this.onFromChange}
            value={props.model.from}
            primary
            left="от"
            hasError={!!fromError}
          />
          {fromError}
        </div>
      </div>

      <div className="col-md-6" >
        <div className={toError ? 'form-group has-error' : 'form-group'}>
          <label>&nbsp;</label>
          <InputGroup
            disabled={props.model.disabled}
            onChange={this.onToChange}
            value={props.model.to}
            primary
            left="до"
            hasError={!!toError}
          />
          {toError}
        </div>
      </div>
    </div>);
  }
}

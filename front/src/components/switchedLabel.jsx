import React from 'react';
import { Switcher } from 'controls/';

export default class switchedLabel extends React.Component {

  constructor(props) {
    super(props);
    this.onSwitcherChange = this.onSwitcherChange.bind(this);
  }

  onSwitcherChange() {
    const props = this.props;
    props.onChange(!props.disabled);
  }

  render() {
    const props = this.props;
    const pStyle = { marginTop: '7px' };
    if (props.disabled) {
      pStyle.opacity = '0.5';
    }
    return (<div className="row">
      <div className="col-md-1">
        <Switcher
          checked={!props.disabled}
          primary
          on-text="вкл"
          onChange={this.onSwitcherChange}
          off-text="выкл"
        />
      </div>
      <div className="col-md-11" >
        <p style={pStyle}>{props.label}</p>
      </div>

    </div>);
  }
}

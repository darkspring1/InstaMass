import React from 'react';
import { DropdownButton, MenuItem } from 'react-bootstrap';

export default class AccountDropDown extends React.Component {
  constructor(props) {
    super(props);
    this.onSelect = this.onSelect.bind(this);
    this.onBlur = this.onBlur.bind(this);
    this.onFocus = this.onFocus.bind(this);
  }

  onSelect(eventKey, event) {
    this.props.onChange({ id: eventKey }, event);
  }

  onBlur() {
    this.props.onBlur(this.props.selectedAccountModel);
  }

  onFocus() {
    this.props.onFocus(this.props.selectedAccountModel);
  }

  render() {
    const props = this.props;
    const accounts = props.accounts.map(a => <MenuItem key={a.id} eventKey={a.id}>{a.login}</MenuItem>);
    let title = props.placeholder;
    if (props.selectedAccountModel) {
      const selectedAccount = this.props.accounts.find(a => a.id === props.selectedAccountModel.id);
      if (selectedAccount) {
        title = selectedAccount.login;
      }
    }

    return (
      <DropdownButton
        onSelect={this.onSelect}
        bsStyle="default"
        title={title}
        onBlur={this.onBlur}
        onFocus={this.onFocus}
        id={props.id}
      >
        {accounts}
      </DropdownButton>);
  }

}

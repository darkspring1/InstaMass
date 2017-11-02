import React from 'react';
import { DropdownButton, MenuItem } from 'react-bootstrap';

export default class AccountDropDown extends React.Component {
  constructor(props) {
    super(props);
    this.onSelect = this.onSelect.bind(this);
  }

  onSelect(eventKey, event) {
    this.props.onChange(eventKey, event);
  }

  render() {
    const props = this.props;
    const accounts = props.accounts.map(a => <MenuItem key={a.id} eventKey={a.id}>{a.login}</MenuItem>);
    let title = props.placeholder;
    if (props.selectedAccountId) {
      const selectedAccount = this.props.accounts.find(a => a.id === props.selectedAccountId);
      if (selectedAccount) {
        title = selectedAccount.login;
      }
    }
    return (
      <DropdownButton
        onSelect={this.onSelect}
        bsStyle="default"
        title={title}
        id={props.id}
      >
        {accounts}
      </DropdownButton>);
  }

}

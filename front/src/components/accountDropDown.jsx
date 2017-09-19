import React from 'react';
import { DropdownButton, MenuItem } from 'react-bootstrap';

export default class AccountDropDown extends React.Component {
  constructor(props) {
    super(props);
    this.onSelect = this.onSelect.bind(this);

    this.state = { title: props.title };
  }

  onSelect(eventKey, event) {
    const selectedAccount = this.getSelectedAccount(eventKey);
    this.setState({ title: selectedAccount.login });
    this.props.onSelect(selectedAccount);
    console.log(this + eventKey + event);
  }

  getSelectedAccount(id) {
    return this.props.accounts.find(a => a.id === id);
  }

  render() {
    const props = this.props;
    const accounts = props.accounts.map(a => <MenuItem key={a.id} eventKey={a.id}>{a.login}</MenuItem>);

    return (
      <DropdownButton
        onSelect={this.onSelect}
        bsStyle="default"
        title={this.state.title}
        id={props.id}
      >
        {accounts}
      </DropdownButton>);
  }

}

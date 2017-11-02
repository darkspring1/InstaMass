import React from 'react';
import { AccountDropDown } from 'components';

export default ({
  input,
  meta: { error },
  selectedAccountId,
  placeholder,
  id,
  accounts
}) => (
  <div className="has-error">
    <input {...input} type="hidden" />
    <div className="input-group">
      <AccountDropDown
        onChange={input.onChange}
        accounts={accounts}
        selectedAccountId={selectedAccountId}
        placeholder={placeholder}
        id={id}
      />
    </div>
    <span className="help-block">{error}</span>
  </div>);

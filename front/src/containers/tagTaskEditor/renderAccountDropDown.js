import React from 'react';
import { AccountDropDown } from 'components';

export default ({
  input,
  meta: { error, touched },
  placeholder,
  id,
  accounts
}) => (
  <div className="has-error">
    <div className="input-group">
      <AccountDropDown
        onChange={input.onChange}
        onBlur={input.onBlur}
        onFocus={input.onFocus}
        accounts={accounts}
        selectedAccountModel={input.value}
        placeholder={placeholder}
        id={id}
      />
    </div>
    <span className="help-block">{ touched && error ? error : ''}</span>
  </div>);

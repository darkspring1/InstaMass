/* eslint no-unused-vars: 0 */
import React from 'react';
import { connect } from 'react-redux';
import { ToastContainer, ToastMessage } from 'react-toastr';


const ToastMessageFactory = React.createFactory(ToastMessage.animation);

class Toastr extends React.Component {

  constructor(props) {
    super(props);
    this.addAlert = this.addAlert.bind(this);
  }

  addAlert() {
    this.container.success(
    'my-title',
    'my-fascinating-toast-message', {
      timeOut: 5000,
      extendedTimeOut: 3000
    });
  }

  render() {
    const props = this.props;
    if (!props.message) {
      return null;
    }
    setTimeout(this.addAlert, 0);
    return (
      <div>
        <ToastContainer
          ref={(input) => { this.container = input; }}
          toastMessageFactory={ToastMessageFactory}
          className="toast-top-right"
        />
        {/* <button onClick={this.addAlert}>GGininder</button> */}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return state.toastr;
}

export default connect(mapStateToProps)(Toastr);

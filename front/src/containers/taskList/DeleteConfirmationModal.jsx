/* eslint react/prefer-stateless-function: 0 */
import React from 'react';
import { Modal, Button } from 'react-bootstrap';

const btnDefaultStyleFix = {
  color: '#333',
  'background-color': '#fff',
  'border-color': '#ccc'
};

export default class DeleteConfirmationModal extends React.Component {

  render() {
    const props = this.props;
    return (
      <Modal.Dialog>
        <Modal.Header bsClass="modal-header bg-warning">
          <i className="ion-android-warning modal-icon" />
          <Modal.Title bsClass="" componentClass="span">ВНИМАНИЕ!</Modal.Title>
        </Modal.Header>

        <Modal.Body bsClass="modal-body text-center">
        Вы уверены, что хотите удалить задачу?
        </Modal.Body>

        <Modal.Footer>
          <Button style={btnDefaultStyleFix} onClick={props.onCancel}>Отмена</Button>
          <Button bsStyle="warning" onClick={props.onConfirm}>Удалить</Button>
        </Modal.Footer>
      </Modal.Dialog>
    );
  }

  }

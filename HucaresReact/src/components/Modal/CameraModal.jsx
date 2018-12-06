import React from 'react';
import { Button, Image, Modal } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from './CameraModal.scss';

export class CameraImageModal extends React.Component {
  render() {
    return (
      <Modal
        trigger={<Button positive>Click here to see camera</Button>}
        className={styles.modalPosition}
      >
        <Modal.Content image>
          <Image src={this.props.imageUrl} />
        </Modal.Content>
      </Modal>
    );
  }
}

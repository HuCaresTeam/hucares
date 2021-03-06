import * as React from 'react';
import { Button, Header, Icon, Modal } from 'semantic-ui-react';
import styles from '../../Modal.scss';

export class CameraDeleteModal extends React.Component {
  state = { modalOpen: false };

  handleOpen = () => this.setState({ modalOpen: true });

  handleClose = () => this.setState({ modalOpen: false });

  render() {
    return (
      <Modal
        className={styles.modalPosition}
        trigger={
          <Button className={styles.buttonWidth} negative onClick={this.handleOpen}>
            Delete
          </Button>
        }
        open={this.state.modalOpen}
        onClose={this.handleClose}
        basic
        size="small"
      >
        <Header icon="archive" content="Delete camera" />
        <Modal.Content>
          <h3>Are you sure you want delete camera?</h3>
        </Modal.Content>
        <Modal.Actions>
          <Button basic color="red" inverted onClick={this.handleClose}>
            <Icon name="remove" /> No
          </Button>
          <Button color="green" inverted>
            <Icon name="checkmark" /> Yes
          </Button>
        </Modal.Actions>
      </Modal>
    );
  }
}

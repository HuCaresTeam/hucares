import * as React from 'react';
import { Button, Header, Icon, Modal } from 'semantic-ui-react';
import axios from 'axios';
import styles from '../../Modal.scss';

export class MLPDeleteModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = { modalOpen: false, callback: props.callback };
  }

  handleOpen = () => this.setState({ modalOpen: true });

  handleClose = () => this.setState({ modalOpen: false });

  handleDelete = plateNumber => {
    axios
      .delete(`${process.env.HUCARES_API_BASE_URL}/api/mlp/delete/all/${plateNumber}`, {
        headers: { 'Access-Control-Allow-Origin': '*' },
      })
      .then(() => {
        this.handleClose();
        this.state.callback();
      });
  };

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
        <Header icon="archive" content="Delete MLP" />
        <Modal.Content>
          <h2 className={styles.h2Style}>Are you sure you want delete this license plate?</h2>
        </Modal.Content>
        <Modal.Actions>
          <Button basic color="red" inverted onClick={this.handleClose}>
            <Icon name="remove" /> No
          </Button>
          <Button color="green" inverted onClick={() => this.handleDelete(this.props.plateNumber)}>
            <Icon name="checkmark" /> Yes
          </Button>
        </Modal.Actions>
      </Modal>
    );
  }
}

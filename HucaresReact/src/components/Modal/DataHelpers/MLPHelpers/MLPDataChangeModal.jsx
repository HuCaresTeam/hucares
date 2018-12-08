import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from '../../Modal.scss';

export class MLPDataChangeModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      PlateNumber: props.data.PlateNumber,
      SearchStartDateTime: props.data.SearchStartDateTime,
      SearchEndDateTime: props.data.SearchEndDateTime,
      Status: props.data.Status,
    };
  }

  render() {
    return (
      <Modal trigger={<Button primary>Update</Button>} className={styles.modalPosition}>
        <Modal.Content>
          <Form>
            <Form.Field>
              <label>Missing License Plate</label>
              <input
                placeholder="License plate"
                value={this.state.PlateNumber}
                onChange={e => this.setState({ PlateNumber: e.target.value })}
              />
            </Form.Field>
            <Form.Field>
              <label>Search Start Date</label>
              <input disabled value={this.state.SearchStartDateTime} />
            </Form.Field>
            <Form.Field>
              <label>Search End Date</label>
              <input disabled value={this.state.SearchEndDateTime} />
            </Form.Field>
            <Form.Field>
              <Checkbox
                label="This license plate was found"
                checked={this.state.Status}
                onChange={e => this.setState({ Status: e.target.value })}
              />
            </Form.Field>
            <Button type="submit">Submit</Button>
          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from '../../Modal.scss';

export class MLPDataChangeModal extends React.Component {
  render() {
    return (
      <Modal trigger={<Button primary>Update</Button>} className={styles.modalPosition}>
        <Modal.Content>
          <Form>
            <Form.Field>
              <label>Missing License Plate</label>
              <input placeholder="License plate" value={this.props.data.PlateNumber} />
            </Form.Field>
            <Form.Field>
              <label>Search Start Date</label>
              <input readOnly value={this.props.data.SearchStartDateTime} />
            </Form.Field>
            <Form.Field>
              <label>Search End Date</label>
              <input readOnly value={this.props.data.SearchEndDateTime} />
            </Form.Field>
            <Form.Field>
              <Checkbox label="This license plate was found" checked={this.props.data.Status} />
            </Form.Field>
            <Button type="submit">Submit</Button>
          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

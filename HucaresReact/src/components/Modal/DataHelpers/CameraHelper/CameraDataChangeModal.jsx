import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from '../../Modal.scss';

export class CameraDataChangeModal extends React.Component {
  render() {
    return (
      <Modal trigger={<Button primary>Update</Button>} className={styles.modalPosition}>
        <Modal.Content>
          <Form>
            <Form.Field>
              <label>Camera Host URL</label>
              <input placeholder="Host URL" value={this.props.data.HostUrl} />
            </Form.Field>
            <Form.Field>
              <label>Latitude</label>
              <input placeholder="Latitude" value={this.props.data.Latitude} />
            </Form.Field>
            <Form.Field>
              <label>Longitude</label>
              <input placeholder="Longitude" value={this.props.data.Longitude} />
            </Form.Field>
            <Form.Field>
              <Checkbox
                label="This camera is trusted source"
                checked={this.props.data.IsTrustedSource}
              />
            </Form.Field>
            <Button type="submit">Submit</Button>
          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

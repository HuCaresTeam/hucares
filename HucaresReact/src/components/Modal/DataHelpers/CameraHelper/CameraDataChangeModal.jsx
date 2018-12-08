import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from '../../Modal.scss';

export class CameraDataChangeModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      HostUrl: props.data.HostUrl,
      Latitude: props.data.Latitude,
      Longitude: props.data.Longitude,
      IsTrustedSource: props.data.IsTrustedSource,
    };
  }

  render() {
    return (
      <Modal trigger={<Button primary>Update</Button>} className={styles.modalPosition}>
        <Modal.Content>
          <Form>
            <Form.Field>
              <label>Camera Host URL</label>
              <input
                placeholder="Host URL"
                value={this.state.HostUrl}
                onChange={e => this.setState({ HostUrl: e.target.value })}
              />
            </Form.Field>
            <Form.Field>
              <label>Latitude</label>
              <input
                placeholder="Latitude"
                value={this.state.Latitude}
                onChange={e => this.setState({ Latitude: e.target.value })}
              />
            </Form.Field>
            <Form.Field>
              <label>Longitude</label>
              <input
                placeholder="Longitude"
                value={this.state.Longitude}
                onChange={e => this.setState({ Longitude: e.target.value })}
              />
            </Form.Field>
            <Form.Field>
              <Checkbox
                label="This camera is trusted source"
                checked={this.state.IsTrustedSource}
                onChange={e => this.setState({ IsTrustedSource: e.target.value })}
              />
            </Form.Field>
            <Button type="submit">Submit</Button>
          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

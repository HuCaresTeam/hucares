import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import axios from 'axios';
import styles from '../../Modal.scss';

export class MLPDataChangeModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      PlateNumber: props.PlateNumber,
      SearchStartDateTime: props.SearchStartDateTime,
      SearchEndDateTime: props.SearchEndDateTime,
      Status: props.Status,
    };
  }

  onSubmitAction() {
    this.close();
    axios
      .post(`${process.env.HUCARES_API_BASE_URL}/api/mlp/insert`, {
        headers: { 'Access-Control-Allow-Origin': '*' },
        plateNumber: this.state.forms[0].value,
        searchStartDatetime: this.convertDate(this.state.forms[1].value),
      })
      .then(() => {
        this.forceUpdate();
        if (this.state.callback) this.state.callback();
      });
  }

  onUpdateAction() {
    axios
      .put(`${process.env.HUCARES_API_BASE_URL}/api/mlp/insert`, {
        headers: { 'Access-Control-Allow-Origin': '*' },
        plateNumber: this.state.forms[0].value,
        searchStartDatetime: this.convertDate(this.state.forms[1].value),
      })
      .then(() => {
        this.forceUpdate();
        if (this.state.callback) this.state.callback();
      });
  }

  currentDateIfNull(date) {
    if (date === undefined) {
      return new Date();
    }
    return new Date(date);
  }

  convertDate(unixDate) {
    const date = this.currentDateIfNull(unixDate);

    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    const hour = date.getHours();
    const minute = date.getMinutes();
    const second = date.getSeconds();

    return `${year}/${month}/${day} ${`0${hour}`.slice(-2)}:${`0${minute}`.slice(
      -2,
    )}:${`0${second}`.slice(-2)}`;
  }

  render() {
    return (
      <Modal
        trigger={
          <Button primary className={this.props.TriggerButtonStyle}>
            {this.props.TriggerButtonText}
          </Button>
        }
        className={styles.modalPosition}
      >
        <Modal.Content>
          <Form>
            <Form.Field>
              <label>Missing license plate</label>
              <input
                placeholder="License Plate"
                value={this.state.PlateNumber}
                onChange={e => this.setState({ PlateNumber: e.target.value })}
              />
            </Form.Field>
            <Form.Field>
              <label>Search start date</label>
              <input
                placeholder="Start date"
                value={this.state.SearchStartDateTime}
                onChange={e => this.setState({ SearchStartDateTime: e.target.value })}
              />
            </Form.Field>
            <Form.Field>
              <label>Search end date</label>
              <input
                placeholder="End date"
                value={this.state.SearchEndDateTime}
                onChange={e => this.setState({ SearchEndDateTime: e.target.value })}
              />
            </Form.Field>
            <Form.Field>
              <Checkbox
                label="This car has been found"
                checked={this.state.Status}
                onChange={e => this.setState({ Status: e.target.value })}
              />
            </Form.Field>
            <Button
              type="submit"
              onClick={() => (this.props.ForUpdate ? this.onSubmitAction() : this.onUpdateAction())}
            >
              Submit
            </Button>
          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

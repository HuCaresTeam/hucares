import React from 'react';
import axios from 'axios';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import DayPickerInput from 'react-day-picker/DayPickerInput';
import styles from './Modal.scss';
import 'react-day-picker/lib/style.css';

export class InfoEditingModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      open: false,
      forms: props.data.formFields,
      checkboxes: props.data.checkboxes,
      callback: props.callback,
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

  close = () => {
    this.setState({ open: false });
  };

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
    const { open } = this.state;

    return (
      <Modal
        trigger={
          <Button
            primary
            className={this.props.data.triggerButtonStyle}
            onClick={() => {
              this.setState({ open: true });
            }}
          >
            {this.props.data.triggerButtonText}
          </Button>
        }
        className={styles.modalPosition}
        open={open}
        onClose={this.close}
      >
        <Modal.Content>
          <Form>
            {this.state.forms.map(form => (
              <Form.Field key={form.id}>
                <label>{form.label}</label>
                {!form.isDate && (
                  <input
                    placeholder={form.placeholder}
                    value={form.value}
                    onChange={edit => {
                      this.state.forms[form.id].value = edit.target.value;
                      this.forceUpdate();
                    }}
                  />
                )}
                {form.isDate && (
                  <DayPickerInput
                    value={form.value}
                    onDayChange={edit => {
                      this.state.forms[form.id].value = edit;
                      this.forceUpdate();
                    }}
                  />
                )}
              </Form.Field>
            ))}

            {(this.state.checkboxes !== undefined || this.state.checkboxes.length !== 0) && (
              <Form.Field>
                {this.state.checkboxes.map(check => (
                  <Checkbox
                    key={check.id}
                    label={check.label}
                    checked={check.value}
                    onChange={() => {
                      this.state.checkboxes[check.id].value = !this.state.checkboxes[check.id]
                        .value;
                      this.forceUpdate();
                    }}
                  />
                ))}
              </Form.Field>
            )}

            <Button
              type="submit"
              onClick={() => {
                this.onSubmitAction();
              }}
            >
              {this.props.data.submitButtonText}
            </Button>
            <Button negative type="cancel" onClick={this.close}>
              {this.props.data.cancelButtonText}
            </Button>
          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

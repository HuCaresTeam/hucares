import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from './Modal.scss';
import axios from "axios";

export class InfoEditingModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      open: false,
      forms: props.data.formFields,
      checkboxes: props.data.checkboxes,
    };
  }

    close = () => {this.setState({open: false})};


    onSubmitAction() {
      this.close();
      
      axios
          .post(`${process.env.HUCARES_API_BASE_URL}/api/mlp/insert`,
              {
                  headers: { 'Access-Control-Allow-Origin': '*' },
                  plateNumber: this.state.forms[0].value,
                  searchStartDatetime: this.state.forms[1].value
              })
          .then((response) =>
            {
              this.forceUpdate();
            });
  }

  render() {
    const {open} = this.state;

    return (
    <div>
      <Button primary className={this.props.data.triggerButtonStyle}
              onClick={() => {this.setState({open: true})}}>
            {this.props.data.triggerButtonText}
      </Button>

      <Modal
        className={styles.modalPosition}
        open={open}
        onClose={this.close}>

        <Modal.Content>
          <Form>
            {this.state.forms.map(form => (
              <Form.Field key={form.id}>
                <label>{form.label}</label>
                <input
                  placeholder={form.placeholder}
                  value={form.value}
                  onChange={edit => {
                      this.state.forms[form.id].value = edit.target.value;
                      this.forceUpdate();
                  }}
                />
              </Form.Field>
            ))}

            {(this.state.checkboxes !== undefined || this.state.checkboxes.length !== 0) && (
              <Form.Field>
                {this.state.checkboxes.map(check => (
                  <Checkbox
                    key={check.id}
                    label={check.label}
                    checked={check.value}
                    onChange={edit => {
                        this.state.checkboxes[check.id].value = !this.state.checkboxes[check.id].value;
                        this.forceUpdate();
                    }}
                  />
                ))}
              </Form.Field>
            )}

            <Button type="submit" onClick={() => {this.onSubmitAction()}}>
              {this.props.data.submitButtonText}
            </Button>
            <Button negative type="cancel" onClick={this.close}>{this.props.data.cancelButtonText}</Button>
          </Form>
        </Modal.Content>
      </Modal>
    </div>
    );
  }
}

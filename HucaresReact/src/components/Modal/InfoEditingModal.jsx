import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from './Modal.scss';

export class InfoEditingModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      forms: props.data.formFields,
      checkboxes: props.data.checkboxes,
    };
  }

  render() {
    return (
      <Modal
        trigger={
          <Button primary className={this.props.data.triggerButtonStyle}>
            {this.props.data.triggerButtonText}
          </Button>
        }
        className={styles.modalPosition}
      >
        <Modal.Content>
          <Form>
            {this.state.forms.map(form => (
              <Form.Field key={form.id}>
                <label>{form.label}</label>
                <input
                  placeholder={form.placeholder}
                  value={form.value}
                  onChange={edit => (
                    (this.state.forms[form.id].value = edit.target.value), this.forceUpdate()
                  )}
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
                    onChange={edit => (
                      (this.state.checkboxes[check.id].value = !this.state.checkboxes[check.id]
                        .value),
                      this.forceUpdate()
                    )}
                  />
                ))}
              </Form.Field>
            )}

            <Button type="submit">
              {this.props.data.submitButtonText}
            </Button>
            <Button negative type="cancel">{this.props.data.cancelButtonText}</Button>
          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

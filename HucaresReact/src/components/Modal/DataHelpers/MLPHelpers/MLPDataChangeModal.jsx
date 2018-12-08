import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from '../../Modal.scss';

export class MLPDataChangeModal extends React.Component {
  constructor(props) {
    super(props);
    // console.log(props);
    this.state = {
      // PlateNumber: props.data.PlateNumber,
      // SearchStartDateTime: props.data.SearchStartDateTime,
      // SearchEndDateTime: props.data.SearchEndDateTime,
      // Status: props.data.Status,

        forms: props.data.formFields,
        checkboxes: props.data.checkboxes
    };
  }

  render() {
    return (
      <Modal trigger={<Button primary>{this.props.data.triggerButtonText}</Button>} className={styles.modalPosition}>
        <Modal.Content>
          <Form>

              {this.state.forms.map(form => (
                  <Form.Field key={form.id}>
                      <label>{form.label}</label>
                      <input
                          placeholder={form.placeholder}
                          value={form.value}
                          onChange={edit => (
                              this.state.forms[form.id].value = edit.target.value,
                              this.forceUpdate())}/>
                      {console.log(form.value)}
                  </Form.Field>
                  ))}



              {/*<Checkbox*/}
                {/*label="This license plate was found"*/}
                {/*checked={this.state.Status}*/}
                {/*onChange={e => this.setState({ Status: e.target.value })}*/}
              {/*/>*/}
            {/*</Form.Field>*/}

            <Button type="submit">{this.props.data.cancelButtonText}</Button>

          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

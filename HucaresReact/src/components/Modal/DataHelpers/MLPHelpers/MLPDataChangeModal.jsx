import React from 'react';
import { Button, Modal, Form, Checkbox } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import styles from '../../Modal.scss';

export class MLPDataChangeModal extends React.Component {
  constructor(props) {
    super(props);
    console.log(props);
    this.state = {
      // PlateNumber: props.data.PlateNumber,
      // SearchStartDateTime: props.data.SearchStartDateTime,
      // SearchEndDateTime: props.data.SearchEndDateTime,
      // Status: props.data.Status,

        Forms: props.data.FormFields
    };
  }

  render() {
    return (
      <Modal trigger={<Button primary>{this.props.data.TriggerButtonText}</Button>} className={styles.modalPosition}>
        <Modal.Content>
          <Form>

              {this.state.Forms.map(form => (
                  <Form.Field key={form.Id}>
                      <label>{form.Label}</label>
                      <input
                          placeholder={form.Placeholder}
                          value={form.Value}
                          onChange={edit => (
                              this.state.Forms[form.Id].Value = edit.target.value,
                              this.forceUpdate())}/>
                      {console.log(form.Value)}
                  </Form.Field>
                  ))}

            {/*<Form.Field>*/}
              {/*<label>Missing License Plate</label>*/}
              {/*<input*/}
                {/*placeholder="License plate"*/}
                {/*value={this.state.PlateNumber}*/}
                {/*onChange={e => this.setState({ PlateNumber: e.target.value })}*/}
              {/*/>*/}
            {/*</Form.Field>*/}
            {/*<Form.Field>*/}
              {/*<label>Search Start Date</label>*/}
              {/*<input disabled value={this.state.SearchStartDateTime} />*/}
            {/*</Form.Field>*/}
            {/*<Form.Field>*/}
              {/*<label>Search End Date</label>*/}
              {/*<input disabled value={this.state.SearchEndDateTime} />*/}
            {/*</Form.Field>*/}
            {/*<Form.Field>*/}
                {/**/}
              {/*<Checkbox*/}
                {/*label="This license plate was found"*/}
                {/*checked={this.state.Status}*/}
                {/*onChange={e => this.setState({ Status: e.target.value })}*/}
              {/*/>*/}
            {/*</Form.Field>*/}

            <Button type="submit">{this.props.data.CancelButtonText}</Button>

          </Form>
        </Modal.Content>
      </Modal>
    );
  }
}

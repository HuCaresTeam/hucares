import React from 'react';
import { Table } from 'semantic-ui-react';

import styles from './MLPTable.scss';

export class MLPTable extends React.Component {
  render() {
    return (
      <div className={styles.mlpTable}>
        <Table celled padded>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Numeris</Table.HeaderCell>
              <Table.HeaderCell>Paieškos data</Table.HeaderCell>
              <Table.HeaderCell>Radimo data</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            <Table.Row>
              <Table.Cell>GZA:150</Table.Cell>
              <Table.Cell singleLine>2018-05-04</Table.Cell>
              <Table.Cell>2018-08-15</Table.Cell>
            </Table.Row>
          </Table.Body>
        </Table>
      </div>
    );
  }
}

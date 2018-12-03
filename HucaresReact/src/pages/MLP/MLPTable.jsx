import React from 'react';
import { Table } from 'semantic-ui-react';

import styles from './MLPTable.scss';
import mlpMock from '../../mocks/mlp';

export class MLPTable extends React.Component {
  render() {
    return (
      <div className={styles.mlpTable}>
        <Table celled padded>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>License plate</Table.HeaderCell>
              <Table.HeaderCell>Search plate date</Table.HeaderCell>
              <Table.HeaderCell>Detected plate date</Table.HeaderCell>
              <Table.HeaderCell>Is found</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {mlpMock.map(obj => (
              <Table.Row>
                <Table.Cell key={obj.PlateNumber}>{obj.PlateNumber}</Table.Cell>
                <Table.Cell key={obj.SearchStartDateTime}>{obj.SearchStartDateTime}</Table.Cell>
                <Table.Cell key={obj.SearchEndDateTime}>
                  {obj.SearchEndDateTime ? obj.SearchEndDateTime : `Not found`}
                </Table.Cell>
                <Table.Cell key={obj.PlateNumber}>{obj.Status ? `Found` : `Not found`}</Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table>
      </div>
    );
  }
}

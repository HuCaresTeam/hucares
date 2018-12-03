import React from 'react';
import { Table } from 'semantic-ui-react';
import styles from './DLPTable.scss';

const DLPTable = () => (
  <div className={styles.dlpTable}>
    <Table celled padded>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Numeris</Table.HeaderCell>
          <Table.HeaderCell>Radimo data</Table.HeaderCell>
          <Table.HeaderCell>Kameros adresas</Table.HeaderCell>
          <Table.HeaderCell>Patikimas šaltinis</Table.HeaderCell>
          <Table.HeaderCell>Patikimimumas %</Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        <Table.Row>
          <Table.Cell>RRR:150</Table.Cell>
          <Table.Cell>2018-05-10 17:44:15</Table.Cell>
          <Table.Cell>J. Basanavičiaus gatvė 8</Table.Cell>
          <Table.Cell>Patikima</Table.Cell>
          <Table.Cell>80%</Table.Cell>
        </Table.Row>
      </Table.Body>
    </Table>
  </div>
);

export default DLPTable;

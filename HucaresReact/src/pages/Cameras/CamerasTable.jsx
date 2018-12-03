import React from 'react';
import { Table } from 'semantic-ui-react';
import styles from './CamerasTable.scss';

const CamerasTable = () => (
  <div className={styles.camerasTable}>
    <Table celled padded>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Kameros adresas</Table.HeaderCell>
          <Table.HeaderCell>Ilguma</Table.HeaderCell>
          <Table.HeaderCell>Platuma</Table.HeaderCell>
          <Table.HeaderCell>Patikimimumas šaltinis</Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        <Table.Row>
          <Table.Cell>J. Basanavičiaus gatvė 8</Table.Cell>
          <Table.Cell>40.854885</Table.Cell>
          <Table.Cell>-88.081807</Table.Cell>
          <Table.Cell>Patikimas</Table.Cell>
        </Table.Row>
      </Table.Body>
    </Table>
  </div>
);

export default CamerasTable;

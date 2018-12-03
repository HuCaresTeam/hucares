import React from 'react';
import { Table } from 'semantic-ui-react';
import styles from './CamerasTable.scss';
import mock from '../../mocks/camera';

export class CamerasTable extends React.Component {
  render() {
    return (
      <div className={styles.camerasTable}>
        <Table celled padded>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Kameros URL</Table.HeaderCell>
              <Table.HeaderCell>Ilguma</Table.HeaderCell>
              <Table.HeaderCell>Platuma</Table.HeaderCell>
              <Table.HeaderCell>Patikimas šaltinis</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {mock.map(obj => (
              <Table.Row>
                <Table.Cell key={obj.HostUrl}>{obj.HostUrl}</Table.Cell>
                <Table.Cell key={obj.Latitude}>{obj.Latitude}</Table.Cell>
                <Table.Cell key={obj.Longitude}>{obj.Longitude}</Table.Cell>
                <Table.Cell key={obj.IsTrustedSource}>
                  {obj.IsTrustedSource ? `Patikima` : `Nepatikima`}
                </Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table>
      </div>
    );
  }
}

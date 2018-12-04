import React from 'react';
import { Table } from 'semantic-ui-react';
import styles from './CamerasTable.scss';
import mock from '../../mocks/camera';
import Pagination from '../../components/Pagination/Pagination';

export class CamerasTable extends React.Component {
  render() {
    return (
      <div className={styles.camerasTable}>
        <Table celled padded>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Camera URL</Table.HeaderCell>
              <Table.HeaderCell>Latitude</Table.HeaderCell>
              <Table.HeaderCell>Longitude</Table.HeaderCell>
              <Table.HeaderCell>Is trusted source</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {mock.map(obj => (
              <Table.Row key={obj.Id}>
                <Table.Cell>{obj.HostUrl}</Table.Cell>
                <Table.Cell>{obj.Latitude}</Table.Cell>
                <Table.Cell>{obj.Longitude}</Table.Cell>
                <Table.Cell>{obj.IsTrustedSource ? `Trusted` : `Not trusted`}</Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>

          <Table.Footer>
            <Table.Row>
              <Table.HeaderCell colSpan="4">
                <Pagination itemList={mock}/>
              </Table.HeaderCell>
            </Table.Row>
          </Table.Footer>
        </Table>
      </div>
    );
  }
}

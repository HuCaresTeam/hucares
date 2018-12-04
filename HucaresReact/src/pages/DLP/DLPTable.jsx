import React from 'react';
import { Table } from 'semantic-ui-react';
import Pagination from '../../components/Pagination/Pagination';
import styles from './DLPTable.scss';
import dlpMock from '../../mocks/dlp';

export class DLPTable extends React.Component {
  render() {
    return (
      <div className={styles.dlpTable}>
        <Table celled padded>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>License plate</Table.HeaderCell>
              <Table.HeaderCell>Detected plate date</Table.HeaderCell>
              <Table.HeaderCell>Image URL</Table.HeaderCell>
              <Table.HeaderCell>Confidence %</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {dlpMock.map(obj => (
              <Table.Row key={obj.Id}>
                <Table.Cell>{obj.PlateNumber}</Table.Cell>
                <Table.Cell>{obj.DetectedDateTime}</Table.Cell>
                <Table.Cell>{obj.ImgUrl}</Table.Cell>
                <Table.Cell>{obj.Confidence}</Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>

          <Table.Footer>
            <Table.Row>
              <Table.HeaderCell colSpan="4">
                <Pagination />
              </Table.HeaderCell>
            </Table.Row>
          </Table.Footer>
        </Table>
      </div>
    );
  }
}

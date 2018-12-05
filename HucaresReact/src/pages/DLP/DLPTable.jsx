import React from 'react';
import { Pagination, Table } from 'semantic-ui-react';
import styles from './DLPTable.scss';
import dlpMock from '../../mocks/dlp';

const chunkArray = (array, size) => {
  if (!array) return [];
  if (!array.length) return [];

  const newArray = [...array];
  const sets = Math.ceil(newArray.length / size);
  const setsArray = Array.from(Array(sets));

  return setsArray.map(() => newArray.splice(0, size));
};

export class DLPTable extends React.Component {
  state = { activePage: 0 };

  getPaginatedData() {
    return chunkArray(dlpMock, 2);
  }

  handlePaginationChange = (e, { activePage }) => this.setState({ activePage });

  render() {
    const { activePage } = this.state;
    const data = this.getPaginatedData();

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
            {!!data &&
              !!data[activePage - 1] &&
              data[activePage - 1].map(obj => (
                <Table.Row key={obj.Id}>
                  <Table.Cell>{obj.PlateNumber}</Table.Cell>
                  <Table.Cell>{obj.DetectedDateTime}</Table.Cell>
                  <Table.Cell>{obj.ImgUrl}</Table.Cell>
                  <Table.Cell>{obj.Confidence}</Table.Cell>
                </Table.Row>
              ))}
          </Table.Body>

          <Pagination
            activePage={activePage}
            firstItem={null}
            lastItem={null}
            pointing
            secondary
            totalPages={data.length}
            onPageChange={this.handlePaginationChange}
          />
        </Table>
      </div>
    );
  }
}

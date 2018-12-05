import React from 'react';
import { Pagination, Table } from 'semantic-ui-react';
import styles from './MLPTable.scss';
import mlpMock from '../../mocks/mlp';
import { chunkArray } from '../../utils/Array';

export class MLPTable extends React.Component {
  state = { activePage: 1 };

  getPaginatedData() {
    return chunkArray(mlpMock, 13);
  }

  handlePaginationChange = (e, { activePage }) => this.setState({ activePage });

  render() {
    const { activePage } = this.state;
    const data = this.getPaginatedData();

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
            {!!data &&
              !!data[activePage - 1] &&
              data[activePage - 1].map(obj => (
                <Table.Row key={obj.Id}>
                  <Table.Cell>{obj.PlateNumber}</Table.Cell>
                  <Table.Cell>{obj.SearchStartDateTime}</Table.Cell>
                  <Table.Cell>
                    {obj.SearchEndDateTime ? obj.SearchEndDateTime : `Not found`}
                  </Table.Cell>
                  <Table.Cell>{obj.Status ? `Found` : `Not found`}</Table.Cell>
                </Table.Row>
              ))}
          </Table.Body>

          <Table.Footer>
            <Table.Row>
              <Table.HeaderCell colSpan="4">
                <Pagination
                  activePage={activePage}
                  firstItem={null}
                  lastItem={null}
                  pointing
                  secondary
                  totalPages={data.length}
                  onPageChange={this.handlePaginationChange}
                />
              </Table.HeaderCell>
            </Table.Row>
          </Table.Footer>
        </Table>
      </div>
    );
  }
}

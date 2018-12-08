import React from 'react';
import { Table } from 'semantic-ui-react';
import styles from './MLPTable.scss';
import mlpMock from '../../mocks/mlp';
import { chunkArray } from '../../utils/Array';
import PaginationContainer from '../../components/Pagination/Pagination';
import { MLPDeleteModal } from '../../components/Modal/DataHelpers/MLPHelpers/MLPDeleteModal';
import { InfoEditingModal } from '../../components/Modal/InfoEditingModal';

export class MLPTable extends React.Component {
  state = { activePage: 1 };

  getPaginatedData() {
    return chunkArray(mlpMock, 10);
  }

  createModalInfo(infoToInsert) {
      return {
          triggerButtonText: "Update",
          modalHeaderText: "Missing License Plate",
          formFields: [
              {
                  id: 0,
                  label: "Missing plate number",
                  placeHolderText: "plate number",
                  value: infoToInsert[0]
              }
          ],
          checkboxes: [
              {
                  id: 0,
                  label: "This license plate has been found",
                  value: infoToInsert[1]
              }

          ],
          submitButtonText: "Submit",
          cancelButtonText: "Cancel",
      };
  };

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
              <Table.HeaderCell>Action</Table.HeaderCell>
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

                  <Table.Cell>
                    <InfoEditingModal data={this.createModalInfo([obj.PlateNumber, obj.Status])} />
                    <MLPDeleteModal/>
                  </Table.Cell>
                </Table.Row>
              ))}
          </Table.Body>

          <Table.Footer>
            <Table.Row>
              <Table.HeaderCell colSpan="4">
                <PaginationContainer
                  activePage={activePage}
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

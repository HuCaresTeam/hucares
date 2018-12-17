import React from 'react';
import { Table } from 'semantic-ui-react';
import axios from 'axios';
import styles from './MLPTable.scss';
import { chunkArray } from '../../utils/Array';
import { formatDate } from '../../utils/FormatDate';
import PaginationContainer from '../../components/Pagination/Pagination';
import { MLPDeleteModal } from '../../components/Modal/DataHelpers/MLPHelpers/MLPDeleteModal';
import { MLPDataChangeModal } from '../../components/Modal/DataHelpers/MLPHelpers/MLPDataChangeModal';

export class MLPTable extends React.Component {
  state = {
    activePage: 1,
    data: [],
  };

  componentDidMount() {
    this.downloadData();
  }

  handlePaginationChange = (e, { activePage }) => this.setState({ activePage });

  downloadData() {
    axios
      .get(`${process.env.HUCARES_API_BASE_URL}/api/mlp/all`, {
        headers: { 'Access-Control-Allow-Origin': '*' },
      })
      .then(res => {
        const data = chunkArray(res.data, window.innerHeight > 800 ? 10 : 6);
        this.setState({ data });
      })
      .catch(() => {
        this.setState({ data: [] });
      });
  }

  render() {
    const { activePage } = this.state;
    const mlpData = this.state.data;
    const triggerButtonUpdate = 'Update';
    const triggerButtonNew = 'Add new';
    const triggerButtonStyle = 'ui right floated button';
    const forUpdate = false;

    return (
      <div className={styles.mlpTable}>
        <Table celled padded>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>License plate</Table.HeaderCell>
              <Table.HeaderCell>Search start date</Table.HeaderCell>
              <Table.HeaderCell>Search close date</Table.HeaderCell>
              <Table.HeaderCell>Action</Table.HeaderCell>
            </Table.Row>
          </Table.Header>

          <Table.Body>
            {!!mlpData &&
              !!mlpData[activePage - 1] &&
              mlpData[activePage - 1].map(obj => (
                <Table.Row key={obj.Id}>
                  <Table.Cell>{obj.PlateNumber}</Table.Cell>
                  <Table.Cell>{formatDate(obj.SearchStartDateTime)}</Table.Cell>
                  <Table.Cell>
                    {obj.SearchEndDateTime ? obj.SearchEndDateTime : `Not found`}
                  </Table.Cell>

                  <Table.Cell>
                    <MLPDataChangeModal
                      PlateNumber={obj.PlateNumber}
                      SearchStartDateTime={obj.SearchStartDateTime}
                      SearchEndDateTime={obj.SearchEndDateTime}
                      Status={obj.Status}
                      FromUpdate={forUpdate}
                      TriggerButtonText={triggerButtonUpdate}
                    />
                    <MLPDeleteModal
                      plateNumber={obj.PlateNumber}
                      callback={() => this.downloadData()}
                    />
                  </Table.Cell>
                </Table.Row>
              ))}
          </Table.Body>

          <Table.Footer>
            <Table.Row>
              <Table.HeaderCell colSpan="4">
                <PaginationContainer
                  activePage={activePage}
                  totalPages={mlpData.length}
                  onPageChange={this.handlePaginationChange}
                />

                <MLPDataChangeModal
                  PlateNumber=""
                  SearchStartDateTime=""
                  SearchEndDateTime=""
                  IsFound=""
                  TriggerButtonText={triggerButtonNew}
                  TriggerButtonStyle={triggerButtonStyle}
                  callback={() => this.downloadData()}
                />
              </Table.HeaderCell>
            </Table.Row>
          </Table.Footer>
        </Table>
      </div>
    );
  }
}

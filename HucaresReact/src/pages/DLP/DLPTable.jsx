import React from 'react';
import { Table } from 'semantic-ui-react';
import axios from 'axios';
import styles from './DLPTable.scss';
import { chunkArray } from '../../utils/Array';
import PaginationContainer from '../../components/Pagination/Pagination';
import { CameraImageModal } from '../../components/Modal/CameraModal';



export class DLPTable extends React.Component {
  state = { activePage: 1,
            loading: true};

  componentDidMount(){
    axios.get("http://www.json-generator.com/api/json/get/clwGCDqzIi?indent=2")
        .then(response => response.data)
        .then(data => {
            this.setState({
                dlpData: data,
                loading: false,
            });
        })
        .catch(() => {
            this.setState({
                dlpData: false,
                loading: false,
            });
        });
  }

  getPaginatedData() {
    return chunkArray(this.state.dlpData, 10);
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
                  <Table.Cell>
                    <CameraImageModal imageUrl={obj.ImgUrl} />
                  </Table.Cell>
                  <Table.Cell>{obj.Confidence.toFixed(2)}</Table.Cell>
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

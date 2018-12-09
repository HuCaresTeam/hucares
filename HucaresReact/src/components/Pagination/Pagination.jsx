import React from 'react';
import { Pagination } from 'semantic-ui-react';

export default class PaginationContainer extends React.Component {
  render() {
    return (
      <Pagination
        activePage={this.props.activePage}
        firstItem={null}
        lastItem={null}
        pointing
        secondary
        totalPages={this.props.totalPages}
        onPageChange={this.props.onPageChange}
      />
    );
  }
}

import React from 'react';
import { Pagination } from 'semantic-ui-react';

export default class PaginationContainer extends React.Component {
  render() {
    return (
      <Pagination
        defaultActivePage={1}
        firstItem={null}
        lastItem={null}
        pointing
        secondary
        totalPages={3}
        onPageChange={() => this.props.changePage(2)}
      />
    );
  }
}

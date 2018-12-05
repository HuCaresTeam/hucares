import React from 'react';
import { Pagination } from 'semantic-ui-react';

export default class PaginationContainer extends React.Component {
  // TODO Move Pagination logic from components here

  render() {
    return (
      <Pagination
        activePage={this.activePage}
        firstItem={null}
        lastItem={null}
        pointing
        secondary
        totalPages={this.props.totalPages}
        onPageChange={console.log('lol')}
      />
    );
  }
}

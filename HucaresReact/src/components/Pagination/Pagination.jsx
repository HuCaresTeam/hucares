import React from 'react';
import { Pagination } from 'semantic-ui-react';

const PaginationContainer = () => (
  <Pagination
    defaultActivePage={1}
    firstItem={null}
    lastItem={null}
    pointing
    secondary
    totalPages={3}
  />
);

export default PaginationContainer;

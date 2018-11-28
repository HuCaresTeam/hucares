import * as React from 'react';

import { BrowserRouter as Router } from 'react-router-dom';
import { Switch } from 'react-router';
import { Route } from './Route';
import { LandingPage } from '../pages/LandingPage/LandingPage';

export default class App extends React.Component {
  render() {
    return (
      <Router>
        <Switch>
          <Route exact path="/" component={LandingPage} />
        </Switch>
      </Router>
    );
  }
}

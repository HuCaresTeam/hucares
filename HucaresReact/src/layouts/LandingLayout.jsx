import React from 'react';
import { Icon, Menu, Segment, Sidebar } from 'semantic-ui-react';
import { BrowserRouter as Router } from 'react-router-dom';
import 'semantic-ui-css/semantic.min.css';
import { Switch } from 'react-router';
import MapContainer from '../components/Map/MapContainer';
import { Route } from '../root/Route';
import DLPTable from '../pages/DLP/DLPTable';
import MLPTable from '../pages/MLP/MLPTable';
import CamerasTable from '../pages/Cameras/CamerasTable';

export class LandingLayout extends React.Component {
  render() {
    return (
      <Sidebar.Pushable as={Segment}>
        <Sidebar as={Menu} animation="push" icon="labeled" direction="" inverted vertical visible>
          <Menu.Item as="a" href="/">
            <Icon name="home" />
            Home
          </Menu.Item>
          <Menu.Item as="a" href="/mlp">
            <Icon name="play" />
            Missing license plates
          </Menu.Item>
          <Menu.Item as="a" href="/dlp">
            <Icon name="shield alternate" />
            Detected License plates
          </Menu.Item>
          <Menu.Item as="a" href="/cameras">
            <Icon name="camera" />
            Cameras
          </Menu.Item>
        </Sidebar>
        <Sidebar.Pusher>
          <Router>
            <Switch>
              <Route exact path="/" component={MapContainer} />
              <Route path="/dlp" component={DLPTable} />
              <Route path="/mlp" component={MLPTable} />
              <Route path="/cameras" component={CamerasTable} />
            </Switch>
          </Router>
        </Sidebar.Pusher>
      </Sidebar.Pushable>
    );
  }
}

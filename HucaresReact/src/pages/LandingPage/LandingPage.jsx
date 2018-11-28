import * as React from 'react';
import Sidebar from '../../components/Sidebar/Sidebar';

export class LandingPage extends React.Component {
  render() {
    return (
      <React.Fragment>
        <div className="tile is-ancestor">
          <div className="tile is-4 is-vertical is-parent">
            <div className="tile is-child box">
              <Sidebar/>
            </div>
          </div>
          <div className="tile is-parent">
            <div className="tile is-child box">
              <p className="title">Three</p>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

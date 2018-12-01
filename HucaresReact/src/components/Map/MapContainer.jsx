import React from 'react';
import { Map, Marker, GoogleApiWrapper } from 'google-maps-react';

export class MapContainer extends React.Component {
  render() {
    return (
      <Map
        google={this.props.google}
        zoom={14}
        style={{ width: '100%', height: '100%', position: 'left' }}
        initialCenter={{
          lat: 40.854885,
          lng: -88.081807,
        }}
      >
        <Marker onClick={this.onMarkerClick} name="Current location" />
      </Map>
    );
  }
}
export default GoogleApiWrapper({
  apiKey: 'AIzaSyCYsE2LLDATDFww6VAi2z5tYpTbHetNDCw',
})(MapContainer);

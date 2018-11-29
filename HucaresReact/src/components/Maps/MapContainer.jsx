import React from 'react';
import { Map, InfoWindow, Marker, GoogleApiWrapper } from 'google-maps-react';

export class MapContainer extends React.Component {
  render() {
    return (
      <Map
        google={this.props.google}
        zoom={14}
        style={{ width: '80%', height: '90%', position: 'relative' }}
        initialCenter={{
          lat: 40.854885,
          lng: -88.081807,
        }}
      >
        <Marker onClick={this.onMarkerClick} name="Current location" />
        <InfoWindow onClose={this.onInfoWindowClose} />
      </Map>
    );
  }
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyCYsE2LLDATDFww6VAi2z5tYpTbHetNDCw',
})(MapContainer);

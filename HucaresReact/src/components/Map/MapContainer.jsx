import React from 'react';
import { InfoWindow, Map, Marker, GoogleApiWrapper } from 'google-maps-react';

export class MapContainer extends React.Component {
  state = {
    showingInfoWindow: false,
    activeMarker: {},
    selectedPlace: {},
  };

  onMarkerClick = (props, marker) =>
    this.setState({
      selectedPlace: props,
      activeMarker: marker,
      showingInfoWindow: true,
    });

  onMapClicked = () => {
    if (this.state.showingInfoWindow) {
      this.setState({
        showingInfoWindow: false,
        activeMarker: null,
      });
    }
  };

  render() {
    return (
      <Map
        google={this.props.google}
        onClick={this.onMapClicked}
        zoom={14}
        style={{ width: '100%', height: '100%', position: 'left' }}
        initialCenter={{
          lat: 54.7000898,
          lng: 25.1125082,
        }}
      >
        <Marker
          name="First Camera"
          onClick={this.onMarkerClick}
          position={{ lat: 54.67100196, lng: 25.22392273 }}
        />
        <Marker />
        <InfoWindow marker={this.state.activeMarker} visible={this.state.showingInfoWindow}>
          <div>
            <h1>{this.state.selectedPlace.name}</h1>
          </div>
        </InfoWindow>
      </Map>
    );
  }
}
export default GoogleApiWrapper({
  apiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY,
})(MapContainer);

import React from 'react';
import { InfoWindow, Map, Marker, GoogleApiWrapper } from 'google-maps-react';

export class MapContainer extends React.Component {
  state = {
    showingInfoWindow: false,
    activeMarker: {},
    selectedPlace: {},
  };

  markers = [
    {
      name: 'Gelezinio vilko ir Ukmerges sankryza.',
      position: { lat: 54.67100196, lng: 25.22392273 },
      isTrusted: true,
    },
    {
      name: 'Ukmerges ir Naugarduko sankryza.',
      position: { lat: 54.33100196, lng: 25.33392273 },
      isTrusted: true,
    },
  ];

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
        {this.markers.map(obj => (
          <Marker
            key={obj.name}
            name={obj.name}
            position={obj.position}
            onClick={this.onMarkerClick}
          />
        ))}

        <InfoWindow marker={this.state.activeMarker} visible={this.state.showingInfoWindow}>
          <div>
            <h1>{this.state.selectedPlace.name}</h1>
            <h1>{this.state.selectedPlace.isTrusted}</h1>
          </div>
        </InfoWindow>

      </Map>
    );
  }
}
export default GoogleApiWrapper({
  apiKey: 'AIzaSyCYsE2LLDATDFww6VAi2z5tYpTbHetNDCw',
})(MapContainer);

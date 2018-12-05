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
      name: 'Gelezinio vilko ir Ukmerges sankriza.',
      url: "https://map.sviesoforai.lt/camera/api/camera/Camera_016.jpg",
      position: { lat: 54.67100196, lng: 25.22392273 },
      isTrusted: true,
    },
    {
      name: 'Ukmerges ir Naugarduko sankriza.',
      url: "https://map.sviesoforai.lt/camera/api/camera/Camera_017.jpg",
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
        }}>

        {this.markers.map(obj => (
          <Marker
            key={obj.url}
            name={obj.name}
            url={obj.url}
            position={obj.position}
            onClick={this.onMarkerClick}
          />
        ))}

        <InfoWindow marker={this.state.activeMarker} visible={this.state.showingInfoWindow}>
          <div>
            {/*<h4>{this.state.selectedPlace.name}</h4>*/}
            <h4><a href={this.state.selectedPlace.url}>{this.state.selectedPlace.url}</a></h4>
            <h6>{this.state.selectedPlace.isTrusted ? "Trusted source" : "Not a trusted source"}</h6>
          </div>
        </InfoWindow>

      </Map>
    );
  }
}
export default GoogleApiWrapper({
  apiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY,
})(MapContainer);

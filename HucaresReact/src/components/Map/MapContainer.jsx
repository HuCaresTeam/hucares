import React from 'react';
import { InfoWindow, Map, Marker, GoogleApiWrapper } from 'google-maps-react';
import { Image, Icon } from 'semantic-ui-react';

export class MapContainer extends React.Component {
  state = {
    showingInfoWindow: false,
    activeMarker: {},
    selectedPlace: {},
  };

  // TODO move to JSON
  markers = [
    {
      name: 'Geležinio Vilko ir Ukmergės g. sankryža',
      url: 'https://map.sviesoforai.lt/camera/api/camera/Camera_016.jpg',
      position: { lat: 54.67100196, lng: 25.22392273 },
      isTrusted: true,
    },
    {
      name: 'Ukmergės g. ir Naugarduko g. sankryža',
      url: 'https://map.sviesoforai.lt/camera/api/camera/Camera_017.jpg',
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
            key={obj.url}
            name={obj.name}
            url={obj.url}
            position={obj.position}
            onClick={this.onMarkerClick}
          />
        ))}

        <InfoWindow marker={this.state.activeMarker} visible={this.state.showingInfoWindow}>
          <div>
            <Image src={this.state.selectedPlace.url} size="large" /> <br />
            <Icon name="share" size="large" />
            {this.state.selectedPlace.isTrusted ? 'Trusted source' : 'Not a trusted source'} <br />
            <Icon name="compass outline" size="large" />
            <b>Location name:</b> {this.state.selectedPlace.name}
          </div>
        </InfoWindow>
      </Map>
    );
  }
}
export default GoogleApiWrapper({
  apiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY,
})(MapContainer);

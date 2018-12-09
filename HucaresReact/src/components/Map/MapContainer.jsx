import React from 'react';
import { InfoWindow, Map, Marker, GoogleApiWrapper } from 'google-maps-react';
import { Image, Icon } from 'semantic-ui-react';
import axios from 'axios';

export class MapContainer extends React.Component {
  state = {
    showingInfoWindow: false,
    activeMarker: {},
    selectedPlace: {},
    data: [],
  };

  componentDidMount() {
    axios
      .get(`http://www.json-generator.com/api/json/get/bUvQtBgGKW?indent=2`)
      .then(res => {
        this.setState({ data: res.data });
      })
      .catch(() => {
        this.setState({ data: [] });
      });
  }

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
    const cameraData = this.state.data;

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
        {cameraData.map(obj => (
          <Marker
            key={obj.Id}
            name="Lalala" // TODO CHANGE
            url={obj.HostUrl}
            position={{ lat: obj.Latitude, lng: obj.Longitude }}
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

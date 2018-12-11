import React from 'react';
import { InfoWindow, Map, Marker, GoogleApiWrapper } from 'google-maps-react';
import { Image, Icon } from 'semantic-ui-react';
import axios from 'axios';
import queryString from 'query-string';

export class MapContainer extends React.Component {
  state = {
    showingInfoWindow: false,
    activeMarker: {},
    selectedPlace: {},
    data: [],
  };

  componentDidMount() {
    if (this.props.location.search) {
      const values = queryString.parse(this.props.location.search);
      this.getMarkersByPlate(values.filter);
    } else {
      this.getAllCameras();
    }
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

  getMarkersByPlate = plateNumber => {
    axios
      .get(`${process.env.HUCARES_API_BASE_URL}/api/camera/all/${plateNumber}`, {
        headers: { 'Access-Control-Allow-Origin': '*' },
      })
      .then(res => {
        this.setState({ data: res.data });
      })
      .catch(() => {
        this.setState({ data: [] });
      });
  };

  getAllCameras = () => {
    axios
      .get(`${process.env.HUCARES_API_BASE_URL}/api/camera/all`, {
        headers: { 'Access-Control-Allow-Origin': '*' },
      })
      .then(res => {
        this.setState({ data: res.data });
      })
      .catch(() => {
        this.setState({ data: [] });
      });
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
          lat: 54.68184,
          lng: 25.268936,
        }}
      >
        {cameraData.map(obj => (
          <Marker
            key={obj.Id}
            name="TO CHANGE"
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

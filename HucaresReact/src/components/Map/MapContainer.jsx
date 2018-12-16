import React from 'react';
import { InfoWindow, Map, Marker, GoogleApiWrapper } from 'google-maps-react';
import { Image, Icon } from 'semantic-ui-react';
import axios from 'axios';
import queryString from 'query-string';
import orangePin from '../../../public/images/maps-pin.png';

export class MapContainer extends React.Component {
  state = {
    showingInfoWindow: false,
    activeMarker: {},
    selectedPlace: {},
    data: [],
    fromDlp: false,
    locationName: '',
  };

  componentDidMount() {
    if (this.props.location.search) {
      const values = queryString.parse(this.props.location.search);
      this.getMarkersByPlate(values.filter);
      this.setState({ fromDlp: true });
    } else {
      this.getAllCameras();
    }
  }

  onMarkerClick = (props, marker) => {
    this.getNameFromLocation(props.position.lat, props.position.lng);

    this.setState({
      selectedPlace: props,
      activeMarker: marker,
      showingInfoWindow: true,
    });
  };

  onMapClicked = () => {
    if (this.state.showingInfoWindow) {
      this.setState({
        showingInfoWindow: false,
        activeMarker: null,
      });
    }
  };

  getNameFromLocation(latitude, longitude) {
    axios
      .get(
        `https://api.opencagedata.com/geocode/v1/json?q=${latitude}+${longitude}&pretty=1&key=${
          process.env.REACT_APP_GEOCODER_API_KEY
        }`,
      )
      .then(res => res.data)
      .then(data =>
        this.setState({
          locationName: `${data.results[0].components.road} ${
            data.results[0].components.house_number
          }, ${data.results[0].components.city}`,
        }),
      );
  }

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

    const highlightedPin = {
      url: orangePin,
      scaledSize: new this.props.google.maps.Size(27, 43), // scaled size
    };

    return (
      <Map
        google={this.props.google}
        onClick={this.onMapClicked}
        zoom={12}
        style={{ width: '100%', height: '100%', position: 'left' }}
        initialCenter={{
          lat: 54.68184,
          lng: 25.268936,
        }}
      >
        {cameraData.map(obj => (
          <Marker
            icon={this.state.fromDlp ? highlightedPin : null}
            key={obj.Id}
            name={this.state.locationName}
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
            <b>Location name:</b> {this.state.locationName}
          </div>
        </InfoWindow>
      </Map>
    );
  }
}
export default GoogleApiWrapper({
  apiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY,
})(MapContainer);

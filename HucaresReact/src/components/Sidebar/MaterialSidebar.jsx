import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import Typography from '@material-ui/core/Typography';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import AddAlarmIcon from '@material-ui/icons/AddAlarm';
import LockOpenIcon from '@material-ui/icons/LockOpen';
import CameraIcon from '@material-ui/icons/Camera';
import MapContainer from '../Maps/MapContainer';

const drawerWidth = 290;

const styles = theme => ({
  root: {
    display: 'flex',
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing.unit,
  },
  toolbar: theme.mixins.toolbar,
});

function ClippedDrawer(props) {
  const { classes } = props;

  return (
    <div className={classes.root}>
      <CssBaseline />
      <AppBar position="fixed" className={classes.appBar}>
        <Toolbar variant="dense">
          <Typography variant="h6" color="inherit" noWrap>
            Hucares
          </Typography>
        </Toolbar>
      </AppBar>
      <Drawer
        className={classes.drawer}
        variant="permanent"
        classes={{
          paper: classes.drawerPaper,
        }}
      >
        <div className={classes.toolbar} />
        <List>
          <ListItem button component="a" href="/mlp">
            <ListItemIcon>
              <AddAlarmIcon />
            </ListItemIcon>
            <ListItemText inset primary="Missing license plate" />
          </ListItem>

          <ListItem button component="a" href="/dlp">
            <ListItemIcon>
              <LockOpenIcon />
            </ListItemIcon>
            <ListItemText inset primary="Detected license plate" />
          </ListItem>

          <ListItem button component="a" href="/cameras">
            <ListItemIcon>
              <CameraIcon />
            </ListItemIcon>
            <ListItemText inset primary="Cameras list" />
          </ListItem>
        </List>
      </Drawer>
      <main className={classes.content}>
        <div className={classes.toolbar} />
        <MapContainer />
      </main>
    </div>
  );
}

ClippedDrawer.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(ClippedDrawer);

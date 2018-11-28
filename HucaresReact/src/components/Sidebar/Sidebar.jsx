import * as React from 'react';
import { slide as Menu } from 'react-burger-menu';
import styles from './Sidebar.scss';

export default props => (
  <Menu style={styles}>
    <a className="menu-item" href="/">
      Home
    </a>

    <a className="menu-item" href="/laravel">
      Laravel
    </a>

    <a className="menu-item" href="/angular">
      Angular
    </a>

    <a className="menu-item" href="/react">
      React
    </a>

    <a className="menu-item" href="/vue">
      Vue
    </a>

    <a className="menu-item" href="/node">
      Node
    </a>
  </Menu>
);

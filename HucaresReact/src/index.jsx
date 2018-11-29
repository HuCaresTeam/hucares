import * as React from 'react';
import { render } from 'react-dom';
import App from './root/App';
import ClippedDrawer from './components/Sidebar/MaterialSidebar';

render(<ClippedDrawer />, document.getElementById('app'));

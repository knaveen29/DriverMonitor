import DriverActivityDetails from "./components/DriverActivityDetails";
import DataSimulator from "./components/DataSimulator";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import React from 'react';

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/data-simulator',
    element: <DataSimulator />
  },
  {
    path: '/activity-data',
    element: <DriverActivityDetails />
  }
];

export default AppRoutes;

import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <p>Welcome to Driver Monitor Application!</p>
        <p>To help you get started! There are 2 modules in this app.</p>
        <ul>
          <li><strong>Driver activity simulator</strong>. You can choose the number of drivers you want and it can simulate and validate the data for you.</li>
          <li><strong>Driver activty monitor</strong>. You can give a date range and it will give you the list of driver activities and if they have done any violations.</li>
        </ul>
      </div>
    );
  }
}

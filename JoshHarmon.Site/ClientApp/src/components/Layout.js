import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { TopBar } from '../components/Shared/TopBar';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
      return (
          <div>
              <TopBar />
              {this.props.children}
          </div>
    );
  }
}

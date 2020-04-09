import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { TopBar } from '../components/Shared/TopBar';
import { FootBar } from './Shared/FootBar';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
      return (
          <div className="layout">
              <TopBar />
              {this.props.children}
              <FootBar />
          </div>
    );
  }
}
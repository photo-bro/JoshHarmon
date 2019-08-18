import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Splash } from './components/Splash/Splash';
import { Projects } from './components/Projects/Projects';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Splash} />
        <Route exact path='/projects' component={Projects} />
      </Layout>
    );
  }
}

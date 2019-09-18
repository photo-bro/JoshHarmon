import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Splash } from './components/Splash/Splash';
import { Projects } from './components/Projects/Projects';
import { NotFound } from './NotFound';

export default class App extends Component {
    static displayName = App.name;

    render() {
        let exactRoute = window.location.pathname;
        return (
            <Layout>
                <Switch>
                    <Route exact path='/' component={Splash} />
                    <Route exact path='/projects' component={Projects} />
                    <Route render={(p) => <NotFound route={exactRoute} />} />
                </Switch>
            </Layout>
        );
    }
}

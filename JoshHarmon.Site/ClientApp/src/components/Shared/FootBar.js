import React, { Component } from 'react';
import { ConnectIcon } from './ConnectIcon';

export class FootBar extends Component {
    static displayName = FootBar.name;

    constructor(props) {
        super(props);
        this.state = {
            connections: [],
            loading: true,
            loadingMessage: 'Loading...'
        };

        fetch('api/connections', { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    connections: data.connections,
                    loading: false
                });
            });
    }

    static buildConnectIcons(connectModels) {
        return (
            <div class="connections">
                {connectModels.map(model => <ConnectIcon model={model} />)}
            </div>
        );
    }

    render() {
        let icons = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : FootBar.buildConnectIcons(this.state.connections)

        return (
            <footer>
                {icons}
                <div class="builtWith">
                    Constructed with .NET Core + React &nbsp;&nbsp; <b>|</b>&nbsp;&nbsp; Deployed on Raspberry PI &nbsp;&nbsp;<b>|</b>&nbsp;&nbsp; Run with recycled electrons
                    </div>
                <div class="copyright">
                    &copy;2019 Josh Harmon - All rights reserved
                </div>
            </footer>
        );
    }
}
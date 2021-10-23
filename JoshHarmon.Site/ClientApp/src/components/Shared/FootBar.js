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
            <div className="connections flex flex-row flex-wrap space-between full-width full-center m0 ">
                {connectModels.map((model, index) => <ConnectIcon model={model} key={index} />)}
            </div>
        );
    }

    render() {
        let icons = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : FootBar.buildConnectIcons(this.state.connections)

        return (
            <footer>
                <div className="flex flex-column m10">
                    {icons}
                    <br />
                    <div className="built-with font-xs flex flex-dividers  flex-wrap full-center thin m10">
                        <span>Constructed with .NET Core & React </span>
                        <span>&nbsp;Powered by Raspberry PI </span>
                    </div>
                    <div className="white font-xs flex full-center m10">
                        &copy;2021 Josh Harmon - All rights reserved
                </div>
                </div>
            </footer>
        );
    }
}
import React, { Component } from 'react';
import { ConnectIcon } from './ConnectIcon';

export class FootBar extends Component {
    static displayName = FootBar.name;

    constructor(props) {
        super(props);
        this.state = {
            iconModels: props.model.icons
        };
    }

    static buildConnectIcons(iconModels) {
        return (
            <div class="connections">
                {iconModels.map(model => <ConnectIcon model={model} />)}
            </div>
        );
    }

    render() {
        let icons = FootBar.buildConnectIcons(this.state.iconModels)

        return (
            <footer>
                {icons}
                <div class="builtWith">
                    Constructed with .NET Core + React &nbsp;&nbsp; <b>|</b>&nbsp;&nbsp; Deployed with Docker &nbsp;&nbsp;<b>|</b>&nbsp;&nbsp; Run with recycled electrons
                    </div>
                <div class="copyright">
                    &copy;2019 Josh Harmon - All rights reserved
                </div>
            </footer>
        );
    }
}
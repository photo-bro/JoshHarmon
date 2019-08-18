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
            </footer>
        );
    }
}
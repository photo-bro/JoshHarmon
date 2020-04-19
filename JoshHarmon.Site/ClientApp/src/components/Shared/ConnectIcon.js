import React, { Component } from 'react';

export class ConnectIcon extends Component {
    static displayName = ConnectIcon.name;

    constructor(props) {
        super(props);
        this.state = {
            name: props.model.name,
            linkUrl: props.model.linkUrl,
            iconUrl: props.model.iconUrl
        };
    }

    render() {
        return (
            <div className="connect-icon m10">
                <a href={this.state.linkUrl} target="_blank" rel="noopener noreferrer" className="flex full-center">
                    <img src={this.state.iconUrl} alt="connect icon" />
                    <div className="connect-name abs m0 shadow-text-soft">
                        <h1 class="m0 semi-thick">{this.state.name}</h1>
                    </div>
                </a>
            </div>
        );
    }
}
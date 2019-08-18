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
            <div class="connectIcon">
                <a href={this.state.linkUrl}>
                    <img src={this.state.iconUrl} />
                 <div class="connectName">
                    {this.state.name}
                    </div>
                </a>               
            </div>
        );
    }
}
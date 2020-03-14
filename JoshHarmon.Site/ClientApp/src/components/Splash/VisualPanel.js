import React, { Component } from 'react';

export class VisualPanel extends Component {
    static displayName = VisualPanel.name;

    constructor(props) {
        super(props);
        this.state = props;
    }

    render() {
        return (
            <div className="panelContainer" >
                <img src={this.state.model.mediaUrl} />
                <div className="panelContainer-title" >
                    <a href={this.state.model.linkUrl} target={this.state.model.linkUrl[0] != '/' ? '_blank' : ''}>
                        <p>{this.state.model.title}</p>
                    </a>
                </div>
            </div>
        );
    }
}





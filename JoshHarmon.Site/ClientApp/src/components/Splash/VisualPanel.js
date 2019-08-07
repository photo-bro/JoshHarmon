import React, { Component } from 'react';
import { Collapse, Container } from 'reactstrap';

export class VisualPanel extends Component {
    static displayName = VisualPanel.name;

    constructor(props) {
        super(props);
        this.state = props;
    }

    render() {
        return (
            <div class="panelContainer" >
                <img src={this.state.model.mediaUrl} />
                <div class="panelContainer-title" >
                    <a href={this.state.model.linkUrl}>
                        <p>{this.state.model.title}</p>
                    </a>
                </div>
            </div>
        );
    }
}





import React, { Component } from 'react';

export class VisualPanel extends Component {
    static displayName = VisualPanel.name;

    constructor(props) {
        super(props);
        this.state = props;
    }

    render() {
        return (
            <div className="panel-container flex full-center shadow-box-soft" >
                <img src={this.state.model.mediaUrl} alt="visual panel" className="flex full-width full-height cover" />
                <span className="abs m0 glow-box-bright">
                    <a href={this.state.model.linkUrl} target={this.state.model.linkUrl[0] !== '/' ? '_blank' : ''}>
                        <div className="font-xl thin shadow-text-soft">{this.state.model.title}</div>
                    </a>
                </span>
            </div>
        );
    }
}





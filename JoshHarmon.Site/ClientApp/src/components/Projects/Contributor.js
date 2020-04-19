import React, { Component } from 'react';

export class Contributor extends Component {
    static displayName = Contributor.name;

    constructor(props) {
        super(props);

        this.state = {
            name: props.model.name,
            contributions: props.model.contributions,
            url: props.model.url
        };
    }

    render() {
        let name = this.state.url
            ? <a className="thick" href={this.state.url}>{this.state.name}</a>
            : this.state.name;

        return (
            <div className="contributor" >
                {name}
                &nbsp;:&nbsp;{this.state.contributions}&nbsp;contributions
            </div>
        );
    }
}

import React, { Component } from 'react';

export default class Tool extends Component {
    static displayName = Tool.name;

    constructor(props) {
        super(props);
        this.state = {
            name: props.model.name,
            toolType: props.model.toolType,
        };
    }

    render() {
        let styleClass = this.state.toolType === 0
            ? "tool-language"
            : this.state.toolType === 1
                ? "tool-framework"
                : "tool-other";

        return (
            <div className={"tool p5 m2 " + styleClass}>
                {this.state.name}
            </div>
        );
    }
}

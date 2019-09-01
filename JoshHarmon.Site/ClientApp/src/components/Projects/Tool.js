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
            ? "toolLanguage"
            : this.state.toolType === 1
                ? "toolFramework"
                : "toolOther";

        return (
            <div class={"tool " + styleClass}>
                {this.state.name}
            </div>
        );
    }
}

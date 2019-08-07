import React, { Component } from 'react';

export class FootBar extends Component {
    static displayName = FootBar.name;

    constructor(props) {
        super(props);
        this.state = props;
    }

    render() {
        return (
            <footer>
                <h2>Connect</h2>
            </footer>
        );
    }
}
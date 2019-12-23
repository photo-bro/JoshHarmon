import React, { Component } from 'react';


export class AboutMe extends Component {
    static displayName = AboutMe.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class="page">
            <h1>About Me</h1>
            </div>
        );
    }
}
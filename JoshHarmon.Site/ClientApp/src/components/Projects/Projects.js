import React, { Component } from 'react';
import { Project } from './Project';

export class Projects extends Component {
    static displayName = Projects.name;

    constructor() {
        super();
        this.state = this.props;
    }


    render() {
        return (
            <div>
                Projects
                <Project title="Medusa's Bane" mediaUrl="/icon/email-icon-black.png" />
            </div>
        );
    }

}
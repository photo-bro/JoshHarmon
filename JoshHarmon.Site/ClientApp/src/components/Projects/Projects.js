import React, { Component } from 'react';
import { Project } from './Project';

export class Projects extends Component {
    static displayName = Projects.name;

    constructor() {
        super();
        this.state = {
            projectModels: [],
            loading: true,
            loadingMessage: "Loading..."
            };

        fetch('api/projects', { method: 'get' })
            .then(response => response.json())
            .then(data => {               
                this.setState({
                    projectModels: data.projectModels,
                    loading: false
                });
            });

    }

    static buildProjectIcons(projectModels) {
        return (
            <div class="projects">
                {projectModels.map(model => <Project model={model} />)}
            </div>
        );      
    }


    render() {
        let projectIcons = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : Projects.buildProjectIcons(this.state.projectModels);

        return (
            <div>
                {projectIcons}
            </div>
        );
    }

}
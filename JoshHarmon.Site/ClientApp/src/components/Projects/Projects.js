import React, { Component } from 'react';
import { Project } from './Project';

export class Projects extends Component {
    static displayName = Projects.name;

    constructor() {
        super();
        this.state = {
            projects: [],
            loading: true,
            loadingMessage: "Loading..."
            };

        fetch('api/projects', { method: 'get' })
            .then(response => response.json())
            .then(data => {               
                this.setState({
                    projects: data.projects,
                    loading: false
                });
            });

    }

    static buildProjectIcons(projectModels) {
        return (
            <div className="projects">
                {projectModels.map(model => <Project model={model} />)}
            </div>
        );      
    }


    render() {
        let projectIcons = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : Projects.buildProjectIcons(this.state.projects);

        return (
            <div className="page">
                {projectIcons}
            </div>
        );
    }

}
import React, { Component } from 'react';
import { RecentRepoActivity } from './RecentRepoActivity';
import { RepoContributions } from './RepoContributions';

export class ProjectModalDetail extends Component {
    static displayName = ProjectModalDetail.name;

    constructor(props) {
        super(props);

        this.state = {
            repositoryName: props.repositoryName,
            repositoryUrl: props.repositoryUrl,
            name: props.name,
            content: props.content,
            tools: props.tools
        };
    }

    render() {
        return (
            <div className="modal-content flex space-between shadow-box-soft p10">
                <div className="modal-content-item m10">
                    <RecentRepoActivity repoName={this.state.repositoryName} />
                </div>
                <div className="modal-content-item m10">
                    <div className="modal-description flex flex-column full-height space-between">
                        <div className="p10">
                            <a href={this.state.repositoryUrl} >
                                <h1>{this.state.name}</h1>
                            </a>
                            <div className="flex flex-row flex-flow">
                                {this.state.tools}
                            </div>
                            <p>{this.state.content}</p>
                        </div>
                        <div className="p10">
                            <RepoContributions repoName={this.state.repositoryName} />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

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
            <div className="modalContent">
                <div className="modalContentItem">
                    <RecentRepoActivity repoName={this.state.repositoryName} />
                </div>
                <div className="modalContentItem">
                    <div className="modalDescription">
                        <div className="modalDescriptionItem">
                            <a href={this.state.repositoryUrl} >
                                <h1>{this.state.name}</h1>
                            </a>
                            <div className="tools">
                                {this.state.tools}
                            </div>
                            <p>{this.state.content}</p>
                        </div>
                        <div className="modalDescriptionItem">
                            <RepoContributions repoName={this.state.repositoryName} />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

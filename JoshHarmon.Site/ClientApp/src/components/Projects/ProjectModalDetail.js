import React, { Component } from 'react';
import { RecentRepoActivity } from './RecentRepoActivity';
import { RepoContributions } from './RepoContributions';
import { MarkdownView } from '../Shared/MarkdownView'
export class ProjectModalDetail extends Component {
    static displayName = ProjectModalDetail.name;

    constructor(props) {
        super(props);
        this.state = {
            repositoryName: props.repositoryName,
            repositoryUrl: props.repositoryUrl,
            name: props.name,
            content: props.content,
            tools: props.tools,
            readme: ''
        };

        fetch('api/github/' + this.state.repositoryName + '/readme', { method: 'get' })
            .then(data => data.json())
            .then(data => {
                this.setState({
                    readme: data.readme
                })
            })
    }

    render() {
        let readme = this.state.readme
            ? <MarkdownView
                rawText={this.state.readme}
                baseImageUri={null} />
            : <div>
                <a href={this.state.repositoryUrl} >
                    <h1>{this.state.name}</h1>
                    <p>{this.state.content}</p>
                </a>
            </div>;
        return (
            <div className="modal-content flex space-between shadow-box-soft p10">
                <div className="modal-content-item m10">
                    <RecentRepoActivity repoName={this.state.repositoryName} />
                </div>
                <div className="modal-content-item m10">
                    <div className="modal-description flex flex-column full-height space-between">
                        <div className="p10">
                            <div className="flex flex-row flex-flow">
                                {this.state.tools}
                            </div>
                            {readme}
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

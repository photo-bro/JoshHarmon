import React, { Component } from 'react';
import { Contributor } from './Contributor';
import { Commit } from './Commit';

export class RecentRepoActivity extends Component {
    static displayName = RecentRepoActivity.name;

    constructor(props) {
        super(props);

        this.state = {
            repoName: props.repoName,
            stats: {},
            commits: [],
            loadingCommits: true,
            loadingMessage: "Loading..."
        };

        fetch('api/github/' + this.state.repoName + '/commits', { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    commits: data.commits,
                    loadingCommits: false
                })
            })
            .catch(ex => {
                this.setState({
                    commits: null,
                    loadingCommits: false
                })
            });
    }

    static buildCommitsDiv(commitModels) {
        if (!commitModels) return (<div />);
        return (
            <div>
                {commitModels.map(c => <Commit model={c} />)}
            </div>
        );
    }

    render() {
        let commits = this.state.loadingCommits
            ? <h3>{this.state.loadingMessage}</h3>
            : RecentRepoActivity.buildCommitsDiv(this.state.commits);

        return (
            <div class="repoActivity" >
                <h2>Recent Activity</h2>
                {commits}
            </div>
        );
    }
}

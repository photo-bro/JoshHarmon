import React, { Component } from 'react';

export class RepoStats extends Component {
    static displayName = RepoStats.name;

    constructor(props) {
        super(props);

        this.state = {
            repoName: props.repoName,
            stats: {},
            commits: [],
            loadingStats: true,
            loadingCommits: true,
            loadingMessage: "Loading..."
        };

        fetch('api/github/' + this.state.repoName + '/stats', { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    stats: data.stats,
                    loadingStats: false
                })
            })
            .catch(ex => {
                this.setState({
                    stats: null,
                    loadingStats: false
                })
            });


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

    static buildStatsDiv(statsModel) {
        let contributors = statsModel.contributors.map(c =>
            <div>
                <h5> {c.name} </h5>
                {'Total Commits: ' + c.totalCommits}<br />
                {'Avg Weekly Commits: ' + c.weeklyAverageCommits}<br />
                {'Avg Weekly Additions: ' + c.weeklyAverageAdditions}<br />
                {'Avg Weekly Deletions: ' + c.weeklyAverageDeletions}<br />
            </div>
        );

        return (
            <div>
                <h4>Contributors</h4>
                <div>
                    {contributors}
                </div>
            </div>

        );

    }

    static buildCommitsDiv(commitModels) {
        if (!commitModels) return (<div />);
        return (
            <div>
                {commitModels.map(c =>
                    <div>
                        {c.committerName + ' ' + c.dateTime}<br />
                        {c.message}<br />
                        {c.sha}<br />
                    </div>
                )}
            </div>
        );
    }


    render() {
        let stats = this.state.loadingStats
            ? <h3>{this.state.loadingMessage}</h3>
            : RepoStats.buildStatsDiv(this.state.stats);

        let commits = this.state.loadingCommits
            ? <h3>{this.state.loadingMessage}</h3>
            : RepoStats.buildCommitsDiv(this.state.commits);

        return (
            <div class="RepoStats" >
                {stats}
                <h2>Recent Activity</h2>
                {commits}
            </div>
        );
    }
}

import React, { Component } from 'react';
import { Contributor } from './Contributor';

export class RepoContributions extends Component {
    static displayName = RepoContributions.name;

    constructor(props) {
        super(props);

        this.state = {
            repoName: props.repoName,
            stats: {},
            contributors: [],
            loadingStats: false,
            loadingContributors: true,
            loadingMessage: "Loading..."
        };

        /*
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
        */

        fetch('api/github/' + this.state.repoName + '/contributors', { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    contributors: data.contributors,
                    loadingContributors: false
                })
            })
            .catch(ex => {
                this.setState({
                    contributors: null,
                    loadingContributors: false
                })
            });
    }

    static buildStats(statsModel) {

    }


    render() {
        /*let stats = this.state.loadingStats
            ? <h3>{this.state.loadingMessage}</h3>
            : RepoContributions.buildStats(this.state.stats);*/

        let contributors = this.state.loadingContributors
            ? <h3>{this.state.loadingMessage}</h3>
            : this.state.contributors
                ? this.state.contributors.map((c, i) => <Contributor key={i} model={c} />)
                : <div />

        return (
            <div className="contributors">
                <h3>Contributors</h3>
                {contributors}
            </div>
        );
    }
}

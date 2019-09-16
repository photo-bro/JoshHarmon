import React, { Component } from 'react';

export class Commit extends Component {
    static displayName = Commit.name;

    constructor(props) {
        super(props);

        this.state = {
            name: props.model.committerName,
            email: props.model.committerEmail,
            dateTime: props.model.dateTime,
            message: props.model.message,
            sha: props.model.sha,
            url: props.model.url,
        };
    }

    render() {
        return (
            <div class="commit" >
                <a href={this.state.url}>
                    {this.state.sha}{this.state.dateTime}
                </a>
                <br />
                <div class="commitDetails">
                    {this.state.dateTime}
                    &nbsp;-&nbsp;
                    <b>{this.state.name}</b>
                    &nbsp;:&nbsp;
                    <i>{this.state.email}</i>
                </div>
                <p>
                    {this.state.message}
                </p>
            </div>
        );
    }
}
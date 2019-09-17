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
                <div class="commitDetails">
                    {this.state.dateTime.substring(0, 10)}
                    &nbsp;&nbsp;
                    <b>{this.state.name}</b>
                    &nbsp;:&nbsp;
                    <i>{this.state.email}</i>
                </div>
                 <a href={this.state.url}>
                    {this.state.sha}
                </a>
                <p>
                    {this.state.message}
                </p>
            </div>
        );
    }
}
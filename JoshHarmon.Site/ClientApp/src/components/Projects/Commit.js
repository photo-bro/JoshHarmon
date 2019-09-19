import React, { Component } from "react";

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
            url: props.model.url
        };
    }

    render() {
        return (
            <div class='commit'>
                <a href={this.state.url}>
                    <p>{this.state.message}</p>
                    <div class='commitDetails'>
                        <div style={{ minWidth: "fit-content" }}>
                            <b>{this.state.name}</b>
                        </div>
                        <div
                            style={{
                                display: "flex",
                                flexDirection: "row",
                                padding: "0 .5em 0 0"
                            }}
                        >
                            <i>{this.state.email}</i>
                            &nbsp;&nbsp;
                            <div>{this.state.sha}</div>
                        </div>
                    </div>
                </a>
            </div>
        );
    }
}

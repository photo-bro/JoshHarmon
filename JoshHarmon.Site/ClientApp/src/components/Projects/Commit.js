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
            <div className='commit m2 p5'>
                <a href={this.state.url}>
                    <p className="m0">{this.state.message}</p>
                    <div className='semi-thin break-word'>
                        <span style={{ minWidth: "fit-content" }}>
                            <b>{this.state.name}</b> <i>({this.state.email})</i>
                        </span>
                    </div>
                </a>
            </div>
        );
    }
}

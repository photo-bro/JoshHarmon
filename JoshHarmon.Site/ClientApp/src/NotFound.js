import React, { Component } from 'react';

export class NotFound extends Component {
    static displayName = NotFound.name;

    constructor(props) {
        super(props);

        this.state = {
            statusCode: props.statusCode,
            route: props.route,
        };
    }

    render() {
        let error = <h2>There is no resource at '<b>{this.state.route}</b>'</h2>;

        return (
            <div class="page">
                <div class="notFound">
                    <h1><b>Page not found...</b></h1>
                    {error}
                    <a href="/">Return to home</a>
                </div>
            </div>
        );
    }
}
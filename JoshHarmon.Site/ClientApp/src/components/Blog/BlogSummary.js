import React, { Component } from 'react';
import { PrettyDate } from '../Shared/PrettyDate';

export class BlogSummary extends Component {
    static displayName = BlogSummary.name;

    constructor(props) {
        super(props);
        this.state = {
            meta: props.meta
        };
    }

    render() {
        return(
           <div class="blogSummary">
                <a href={"/blog/" + this.state.meta.id}><h1>{this.state.meta.title}</h1></a>
                <p> </p>
                <div class="blogSubtitle">
                    <h3><b>{this.state.meta.author}</b></h3>
                    <i><h3><PrettyDate dateTime={this.state.meta.publishDate} /></h3></i>
                </div>
                <hr />
           </div>
        );
    }

}
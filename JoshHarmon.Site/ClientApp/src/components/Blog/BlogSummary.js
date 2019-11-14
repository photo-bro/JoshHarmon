import React, { Component } from 'react';

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
                <h3>{this.state.meta.author}</h3>
                <h4>{this.state.meta.publishDate}</h4>
                <hr />
           </div>
        );
    }

}
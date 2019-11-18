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

    static articleLink(meta)
    {
        return "/blog/" + meta.id;
    }

    static buildArticleAnchor(meta) {
        if (!meta.bannerMediaPath || meta.bannerMediaPath === '')
        {
            return (
                <a href={BlogSummary.articleLink(meta)}>
                    <h1>{meta.title}</h1>
                </a>
            );
        }

        return(
            <a href={BlogSummary.articleLink(meta)}>
                <img src={meta.bannerMediaPath} />
                <h1>{meta.title}</h1>
            </a>
        );
    }

    render() {
        let articleAnchor = BlogSummary.buildArticleAnchor(this.state.meta);

        return(
           <div class="blogSummary">
                {articleAnchor}
                <div class="blogSubtitle">
                    <h3><b>{this.state.meta.author}</b></h3>
                    <i><h3><PrettyDate dateTime={this.state.meta.publishDate} /></h3></i>
                </div>
                <br />
                <p>{this.state.meta.summary}</p>
                <a href={BlogSummary.articleLink(this.state.meta)}>read more...</a>
                <hr />                
           </div>
        );
    }

}
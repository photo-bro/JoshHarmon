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
        const pubDate = new Date(meta.publishDate);
        const link = '/blog/' +
            pubDate.getFullYear() + '/' +
            (pubDate.getMonth() + 1) + '/' +
            pubDate.getDate() + '/' +
            meta.fileKey;
        return link;
    }

    static buildArticleHeader(meta) {
        if (!meta.bannerMediaPath || meta.bannerMediaPath === '')
        {
            return (
                <div>
                    <a href={BlogSummary.articleLink(meta)}>
                        <h1>{meta.title}</h1>
                    </a>
                    {BlogSummary.buildSubtitleDiv(meta)}
                </div>
            );
        }

        const bannerUri = 'api' + BlogSummary.articleLink(meta) + '/' + meta.bannerMediaPath;

        return(
            <div>
                <a href={BlogSummary.articleLink(meta)}>
                    <h1>{meta.title}</h1>
                </a>
                {BlogSummary.buildSubtitleDiv(meta)}
                <br />
                <a href={BlogSummary.articleLink(meta)}>
                    <img src={bannerUri} />
                </a>
            </div>
        );
    }

    static buildSubtitleDiv(meta) {
        return(
            <div class="blogSubtitle">
                    <h3><b>{meta.author}</b></h3>
                    <i><h3><PrettyDate dateTime={meta.publishDate} /></h3></i>
            </div>
        );
    }


    render() {
        let articleHeader = BlogSummary.buildArticleHeader(this.state.meta);

        return(
           <div class="blogSummary">
                {articleHeader}
                <br />
                <p>{this.state.meta.summary}</p>
                <a href={BlogSummary.articleLink(this.state.meta)}>read more...</a>
                <hr />
           </div>
        );
    }
}
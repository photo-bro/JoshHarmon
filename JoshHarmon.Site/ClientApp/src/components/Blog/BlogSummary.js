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

    static articleLink(meta) {
        const pubDate = new Date(meta.publishDate);
        const link = '/blog/' +
            pubDate.getFullYear() + '/' +
            (pubDate.getMonth() + 1) + '/' +
            pubDate.getDate() + '/' +
            meta.fileKey;
        return link;
    }

    static buildArticleHeader(meta) {
        if (!meta.bannerMediaPath || meta.bannerMediaPath === '') {
            return (
                <div>
                    <a href={BlogSummary.articleLink(meta)}>
                        <h1 className="thick">{meta.title}</h1>
                    </a>
                    {BlogSummary.buildSubtitleDiv(meta)}
                </div>
            );
        }

        const bannerUri = 'api' + BlogSummary.articleLink(meta) + '/' + meta.bannerMediaPath;

        return (
            <div>
                <a href={BlogSummary.articleLink(meta)}>
                    <h1 className="thick grey">{meta.title}</h1>
                </a>
                {BlogSummary.buildSubtitleDiv(meta)}
                <br />
                <a href={BlogSummary.articleLink(meta)}>
                    <img src={bannerUri} alt="Blog article banner" className="full-width cover" />
                </a>
            </div>
        );
    }

    static buildSubtitleDiv(meta) {
        return (
            <div className="blog-subtitle flex ml10 mr10">
                <h3 class="semi-thin mr10"><b>{meta.author}</b></h3>
                <h3 class="thin"><i><PrettyDate dateTime={meta.publishDate} includeDay="True" /></i></h3>
            </div>
        );
    }


    render() {
        let articleHeader = BlogSummary.buildArticleHeader(this.state.meta);

        return (
            <div className="blog-summary shadow-box-soft">
                {articleHeader}
                <br />
                <p>{this.state.meta.summary}</p>
                <br />
                <a className="ml10 grey semi-thick" href={BlogSummary.articleLink(this.state.meta)}>read more...</a>
            </div>
        );
    }
}
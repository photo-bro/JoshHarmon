import React, { Component } from 'react';
import queryString from 'query-string';
import { BlogSummary } from './BlogSummary';

export class Blog extends Component {
    static displayName = Blog.name;
    static defaultLimit = 3;

    constructor(props) {
        super(props);

        const queryValues = queryString.parse(props.location.search);

        this.state = {
            blogMetas: [],
            page: {},
            loading: true,
            loadingMessage: "Loading...",
            limit: queryValues.limit,
            offset: queryValues.offset
        };

        let url = 'api/blog?limit=';
        if (!queryValues.limit)
            url += Blog.defaultLimit;
        else
            url += queryValues.limit;
        if (queryValues.offset)
            url += '&offset=' + queryValues.offset;

        fetch(url, { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    blogMetas: data.data,
                    page: data.page,
                    loading: false
                });
            });
    }

    static buildBlogSummaries(blogMetas) {
        return (
            <div className="blog-summaries flex flex-column full-width ">
                {blogMetas.map((m, i) => <BlogSummary key={i} meta={m} />)}
            </div>
        );
    }

    static buildPagingDiv(page, limit) {
        if (!page)
            return (<div />);

        if (!limit || limit === 0)
            limit = Blog.defaultLimit;
        else
            limit = parseInt(limit, 10);

        let blogBase = '/blog?limit=' + limit + '&';
        let olderPage = '';
        let newerPage = '';

        if (page.offset + limit < page.total)
            olderPage = blogBase + 'offset=' + (page.offset + limit);
        else
            olderPage = '';

        if (page.offset >= limit)
            newerPage += blogBase + 'offset=' + (page.offset - limit);
        else
            newerPage = '';

        if (olderPage === '' && newerPage === '')
            return (
                <div className="blog-page-control flex full-center" />
            );

        let olderAnchor = olderPage === ''
            ? <div style={{ color: '#c5c5c5' }}>oldest</div>
            : <a href={olderPage}>older &gt;&gt;</a>;

        let newerAnchor = newerPage === ''
            ? <div style={{ color: '#c5c5c5' }}>most recent</div>
            : <a href={newerPage}>&lt;&lt; recent</a>

        return (
            <div className="blog-page-control flex full-center">
                {newerAnchor}
                &nbsp; | &nbsp;
                {olderAnchor}
            </div>
        );
    }

    render() {
        let summaries = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : Blog.buildBlogSummaries(this.state.blogMetas);

        let pageDiv = this.state.loading && !this.state.data
            ? <div />
            : Blog.buildPagingDiv(this.state.page, this.state.limit);

        return (
            <div className="page">
                <div className="blog-page">
                    <br />
                    <br />
                    {summaries}
                    {pageDiv}
                    <br />
                    <br />
                </div>
            </div>
        );
    }
}
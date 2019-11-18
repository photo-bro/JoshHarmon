import React, { Component } from 'react';
import queryString from 'query-string';
import { BlogSummary } from './BlogSummary';

export class Blog extends Component {
    static displayName = Blog.name;
    static defaultLimit = 10;

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

        let url = 'api/blog?';
        if (queryValues.limit != '')
            url += 'limit=' + queryValues.limit;
        if (queryValues.offset != '')
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

    static buildBlogSummaries(blogMetas){
        return(
            <div class="blogSummaries">
                {blogMetas.map(m => <BlogSummary meta={m} />)}
            </div>
        );
    }

    static buildPagingDiv(page, limit)
    {
        if (!page)
            return(<div />);

        if (!limit || limit === 0)
            limit = Blog.defaultLimit;

        limit = parseInt(limit);

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
            return(
                <div class="blogPageControl" />
            );

        let olderAnchor = olderPage === ''
            ? <div style={{color: '#c5c5c5'}}>oldest</div>
            : <a href={olderPage}>older &gt;&gt;</a>;

        let newerAnchor = newerPage == ''
            ? <div style={{color: '#c5c5c5'}}>most recent</div>
            : <a href={newerPage}>&lt;&lt; recent</a>        

        return(
            <div class="blogPageControl">
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

        return(
            <div class="page">
                <div class="blogPage">
                    <div class="blogTitle">
                        recent posts:
                    <br />
                    <hr />
                    </div>
                    {summaries}
                    {pageDiv}
                    <hr />
                </div>
            </div>
        );
    }
}
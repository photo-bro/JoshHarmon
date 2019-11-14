import React, { Component } from 'react';
import queryString from 'query-string';
import { BlogSummary } from './BlogSummary';


export class Blog extends Component {
    static displayName = Blog.name;

    constructor(props) {
        super(props);
        
        this.state = {
            blogMetas: [],
            page: {},
            loading: true,
            loadingMessage: "Loading...",
            rawQueryString: props.location.search
            };

        const queryValues = queryString.parse(this.state.rawQueryString);

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

    static buildPagingDiv(page)
    {
        if (!page)
            return(<div />);

        let limit = 10;
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

        let olderAnchor = olderPage === ''
            ? <div />
            : <a href={olderPage}>Older Posts</a>;

        let newerAnchor = newerPage == ''
            ? <div />
            : <a href={newerPage}>Newer Posts</a>

        return(
            <div class="blogPage">
                {olderAnchor}
                {newerAnchor}
            </div>
        );
    }

    render() {
        let summaries = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : Blog.buildBlogSummaries(this.state.blogMetas);

        let pageDiv = this.state.loading && !this.state.data
            ? <div />
            : Blog.buildPagingDiv(this.state.page);

        return(
            <div class="page">
                {summaries}
                {pageDiv}
            </div>
        );
    }
}
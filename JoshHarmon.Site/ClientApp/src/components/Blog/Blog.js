import React, { Component } from 'react';
import { BlogSummary } from './BlogSummary';


export class Blog extends Component {
    static displayName = Blog.name;

    constructor(props) {
        super(props);
       
        this.state = {
            blogMetas: [],
            page: {},
            loading: true,
            loadingMessage: "Loading..."
            };

        fetch('api/blog', { method: 'get' })
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


    render() {
        let summaries = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : Blog.buildBlogSummaries(this.state.blogMetas);

        return(
            <div class="blog">
                {summaries}
            </div>
        );
    }

}
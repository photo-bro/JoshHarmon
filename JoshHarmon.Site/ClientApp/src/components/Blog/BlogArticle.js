import React, { Component } from 'react';

export class BlogArticle extends Component {
    static displayName = BlogArticle.name;

    constructor(props) {
        super(props);
        this.state = {
            id: props.match.params.id,
            article: {},
            loading: true,
            loadingMessage: "Loading..."
        };

        fetch('api/blog/' + this.state.id, { method: 'get' })
            .then(response => response.json())
            .then(data => {               
                this.setState({
                    article: data.data.article,
                    loading: false
                });
            });

    }

    static buildBlogArticle(article)
    {
        return(
            <div class="blogArticle">
                <h1>{article.meta.title}</h1>
                <h3>{article.meta.author}</h3>
                <h4>{article.meta.publishDate}</h4>
                <br / >
                <p>{article.content}</p>
            </div>
        );
    }

    render() {
        let entry = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : BlogArticle.buildBlogArticle(this.state.article);                    

        return(
            <div>{entry}</div>
        );
    }
}

import React, { Component } from 'react';
import { MarkdownView } from '../Shared/MarkdownView';
import { PrettyDate } from '../Shared/PrettyDate';

export class BlogArticle extends Component {
    static displayName = BlogArticle.name;

    constructor(props) {
        super(props);
        this.state = {
            id: props.match.params.id,
            year: props.match.params.year,
            month: props.match.params.month,
            day: props.match.params.day,
            fileKey: props.match.params.fileKey,
            article: {},
            loading: true,
            loadingMessage: "Loading..."
        };

        let fetchUrl = '/api/blog/';
        if (this.state.id)
            fetchUrl += this.state.id;
        else
            fetchUrl += this.state.year + '/' +
                   this.state.month + '/' +
                   this.state.day + '/' +
                   this.state.fileKey;

        console.log(fetchUrl);

        fetch(fetchUrl, { method: 'get' })
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
                <img src={article.meta.bannerMediaPath} />
                <div class="blogHeader">
                    <h1>{article.meta.title}</h1>
                    <div>
                        <h3>{article.meta.author}</h3>
                        <h4><PrettyDate dateTime={article.meta.publishDate} /></h4>
                    </div>
                </div>
                <hr / >
                <div class="blogArticleContent" >
                    <MarkdownView rawText={article.content} />
                </div>
                <hr />
              </div>
        );
    }

    render() {
        let entry = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : BlogArticle.buildBlogArticle(this.state.article);

        return(
            <div class="page">
                {entry}
                <div>
                </div>
            </div>
        );
    }
}

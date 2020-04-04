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

        let fetchUrl = this.GetBaseUri();

        fetch(fetchUrl, { method: 'get' })
            .then(response => response.json())
            .then(data => {               
                this.setState({
                    article: data.data.article,
                    loading: false
                });
            });
    }

    GetBaseUri() {
        let fetchUrl = '/api/blog/';

        if (this.state.id){
            fetchUrl += this.state.id;
            return fetchUrl;
        }
        
        fetchUrl += this.state.year + '/' +
                this.state.month + '/' +
                this.state.day + '/' +
                this.state.fileKey;

        return fetchUrl;
    }

    static buildBlogArticle(article, baseUri)
    {
        const bannerUri = baseUri + '/' + article.meta.bannerMediaPath;
        return(
            <div className="blogArticle">
                <img src={bannerUri} />
                <div className="blogHeader">
                    <h1>{article.meta.title}</h1>
                    <div>
                        <h3>{article.meta.author}</h3>
                        <h4><PrettyDate dateTime={article.meta.publishDate} /></h4>
                    </div>
                </div>
                <hr / >
                <div className="blogArticleContent" >
                    <MarkdownView
                        rawText={article.content}
                        baseImageUri={baseUri}
                    />
                </div>
                <hr />
              </div>
        );
    }

    render() {
        let entry = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : BlogArticle.buildBlogArticle(this.state.article, this.GetBaseUri());

        return(
            <div className="page">
                {entry}
                <div>
                </div>
            </div>
        );
    }
}

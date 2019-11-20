import React, { Component } from 'react';
import ReactMarkdown from 'react-markdown';

export class MarkdownView extends Component {
    static displayName = MarkdownView.name;
    static baseImageUri = '';

    constructor(props) {
        super(props);
        this.state = {
            rawText: props.rawText
        };

        MarkdownView.baseImageUri = this.props.baseImageUri;
    }

    static BuildImageUri(assetKey)
    {
        return MarkdownView.baseImageUri + '/' + assetKey;
    }

    render() {
        let uriTransformFunc = function (uri){
            return MarkdownView.BuildImageUri(uri)
        };

        return(
            <div class="markdown">
                <ReactMarkdown
                    source={this.state.rawText}
                    transformImageUri={uriTransformFunc}
                />
            </div>
        );
    }
}
import React, { Component } from 'react';
import ReactMarkdown from 'react-markdown';

export class MarkdownView extends Component {
    static displayName = MarkdownView.name;

    constructor(props) {
        super(props);
        this.state = {
            rawText: props.rawText
        };
    }

    render() {
        return(
            <div class="markdown">
                <ReactMarkdown source={this.state.rawText} />
            </div>
        );
    }
}
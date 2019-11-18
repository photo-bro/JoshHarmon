import React, { Component } from 'react';

export class PrettyDate extends Component {
    static displayName = PrettyDate.name;

    constructor(props) {
        super(props);
        this.state = {
            rawDate: props.dateTime
        };
    }

    static buildFormattedDateTimeString(rawDate) {
        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        const pubDate = new Date(rawDate);
        const prettyDate = pubDate.toLocaleDateString('en-US', options);

        return prettyDate;
    }

    render() {
        let formattedDate = PrettyDate.buildFormattedDateTimeString(this.state.rawDate);
        return(
            <div class="formattedDateTime">
                {formattedDate}
            </div>
        );
    }
}
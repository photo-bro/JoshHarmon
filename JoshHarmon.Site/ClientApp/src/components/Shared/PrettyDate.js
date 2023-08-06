import React, { Component } from 'react';

export class PrettyDate extends Component {
    static displayName = PrettyDate.name;

    constructor(props) {
        super(props);
        this.state = {
            rawDate: props.dateTime,
            includeDay: props.includeDay === "True",
        };
    }

    static buildFormattedDateTimeString(rawDate, includeDay) {
        let options = { year: 'numeric', month: 'long' };
        if (includeDay) {
            options.day = 'numeric';
            options.weekday = 'long';
        }
        const pubDate = new Date(rawDate);
        const prettyDate = pubDate.toLocaleDateString('en-US', options);

        return prettyDate;
    }

    render() {
        let formattedDate = PrettyDate.buildFormattedDateTimeString(this.state.rawDate, this.state.includeDay);
        return (
            <div>
                {formattedDate}
            </div>
        );
    }
}

import React, { Component } from 'react';

export class DateDelta extends Component {
    static displayName = DateDelta.name;
    static MS = 1000;
    static DAY = DateDelta.MS * 60 * 60 * 24;
    static MONTH = DateDelta.DAY * 30; // FIXME: Rough approximation, make more accurate
    static YEAR = DateDelta.DAY * 365


    constructor(props) {
        super(props);
        this.state = {
            leftDate: props.leftDate,
            rightDate: props.rightDate
        };
    }

    static getDateDelta(left, right) {
        const l = new Date(left);
        const r = new Date(right);
        const diff = Math.abs(r - l);

        const years = Math.floor(diff / DateDelta.YEAR);
        const months = Math.ceil((diff % DateDelta.YEAR) / DateDelta.MONTH);

        let output = '';
        if (years > 0)
            output += years + ' year' + (years > 1 ? 's' : '') + ' and ';
        if (months > 0)
            output += months + ' month' + (months > 1 ? 's' : '');

        return output;
    }

    render() {
        if (!this.state.leftDate || !this.state.rightDate)
            return (<div />);

        const delta = DateDelta.getDateDelta(this.state.leftDate, this.state.rightDate);

        return (
            <div class="dateDelta">
                {delta}
            </div>
        );
    }
}
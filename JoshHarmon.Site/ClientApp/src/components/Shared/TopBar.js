import React, { Component } from 'react';

export class TopBar extends Component {
    static displayName = TopBar.name;

    render() {
        return (
            <header>
                <div className="header-title font-xl semi-thin grey flex full-center shadow-box-sharp shadow-text-sharp">
                    <a href={window.location.origin}>
                        Josh<b>Harmon</b>
                    </a>
                </div>
            </header>
        );
    }
}

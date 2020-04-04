import React, { Component } from 'react';
import { Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class TopBar extends Component {
    static displayName = TopBar.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <header>
                <div className="header-title">
                    <a href={window.location.origin}>
                        Josh<b>Harmon</b>
                    </a>
                </div>
            </header>
        );
    }
}

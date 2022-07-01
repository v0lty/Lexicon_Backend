import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './Menu';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div className="d-flex flex-column overflow-hidden min-vh-100 vh-100">
                <NavMenu />
                <div className="flex-grow-1 overflow-auto p-3">
                    {this.props.children}
                </div>
                <footer className="footer border-top p-3">
                    <span>&copy; {new Date().toLocaleDateString()} ~ Kim Holmer</span>
                </footer>
            </div>
        );
    }
}
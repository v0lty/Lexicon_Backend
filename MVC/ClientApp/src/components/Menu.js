import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-md navbar-light shadow">
                    <Container fluid={true} className="p-0">
                        <NavbarBrand tag={Link} className="navbar-brand text-muted font-weight-bold" to="/">LOGO</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} to="/people">People</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} to="/countries">Countries</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} to="/cities">Cities</NavLink>
                                </NavItem>
            
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
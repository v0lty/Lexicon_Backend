import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { PeopleComponent } from './components/People';
import { CitiesComponent } from './components/Cities';
import { CountriesComponent } from './components/Countries';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/countries' component={CountriesComponent} />
                <Route path='/cities' component={CitiesComponent} />
                <Route path='/people' component={PeopleComponent} />
            </Layout>
        );
    }
}
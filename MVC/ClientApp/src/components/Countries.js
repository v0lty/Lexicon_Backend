import React, { Component, useEffect, useState } from 'react';
import axios from 'axios';
import { TailSpin } from 'react-loader-spinner'
import { CountriesTable } from './CountriesTable'
import { CountryModal } from './CountryModal'
import { isNumeric } from 'jquery';

export class CountriesComponent extends Component {

    baseURL = 'https://localhost:44395/api/';

    constructor(props) {
        super(props);
        this.state = { countries: [], country: null, loading: true, modalShow: false };
        this.showCountryModel = this.showCountryModel.bind(this);
        this.hideCountryModel = this.hideCountryModel.bind(this);
        this.updateContries = this.updateContries.bind(this);
        this.removeCountry = this.removeCountry.bind(this);
        this.submitCountry = this.submitCountry.bind(this);
    }

    componentDidMount() {
        this.updateContries();
    }

    async updateContries() {
        await axios.get(this.baseURL + 'GetCountries').then((response) => {
            if (response.status == 200) {
                this.setState({ countries: JSON.parse(response.data), loading: false });
            }
        })
        .catch((error) => {
            alert(error + '\nMessage: ' + error.response.data.responseText);
            this.setState({ countries: null });
        });
    }

    async removeCountry(id) {
        axios.post(this.baseURL + 'RemoveCountry', null, {
            params: {
                countryId: id
            }
        }).then((response) => {
            this.updateContries();
        })
        .catch(function (error) {
            alert(error + '\nMessage: ' + error.response.data.responseText);
        });
    }

    showCountryModel = (country) => {
        this.setState({ country: country, modalShow: true });
    }

    hideCountryModel = () => {
        this.setState({ country: null, modalShow: false });
    }

    async submitCountry(e) {
        e.preventDefault();
        // same modal & form is used for edit and create, but modal.country is never set on create 
        let editing = isNumeric(e.target.elements['formCountry'].value);

        axios.post(this.baseURL + (editing ? 'EditCountry' : 'CreateCountry'), null, {
            params: {
                countryId: e.target.elements['formCountry'].value,
                countryName: e.target.elements['formName'].value
            }
        }).then((response) => {
            this.updateContries();
            this.hideCountryModel();
        })
        .catch(function (error) {
            alert(error + '\nMessage: ' + error.response.data.responseText);
        });
    }

    render() {

        if (this.state.loading) {
            return (
                <div>
                    <TailSpin color="lightblue" height={100} width={100} />
                    <p>Loading Data...</p>
                </div>
            );
        }
        else {
            return (
                <div>
                    <h1 className="pb-3">Countries</h1>
                    <CountriesTable countries={this.state.countries}
                                    onCreateCountryClick={this.showCountryModel}
                                    onEditCountryClick={this.showCountryModel}
                                    onRemoveCountryClick={this.removeCountry}/>
                    
                    <CountryModal country={this.state.country}
                                  show={this.state.modalShow}
                                  onHide={this.hideCountryModel}
                                  onSubmit={this.submitCountry}/>
                </div>
            );
        }
    }
}

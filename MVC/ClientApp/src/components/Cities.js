import React, { Component, useEffect, useState } from 'react';
import axios from 'axios';
import { TailSpin } from 'react-loader-spinner'
import { CitiesTable } from './CitiesTable'
import { CityModal } from './CityModal'
import { isNumeric } from 'jquery';

export class CitiesComponent extends Component {

    baseURL = 'https://localhost:44395/api/';

    constructor(props) {
        super(props);
        this.state = { cities: [], countries: [], loading: true, modalShow: false };
        this.updateCities = this.updateCities.bind(this);
        this.updateContries = this.updateContries.bind(this);
        this.showCityModal = this.showCityModal.bind(this);
        this.hideCityModal = this.hideCityModal.bind(this);
        this.submitCity = this.submitCity.bind(this);
        this.removeCity = this.removeCity.bind(this);
    }

    componentDidMount() {
        this.updateCities();
    }

    async updateCities() {
        await axios.get(this.baseURL + 'GetCities').then((response) => {
            if (response.status == 200) {
                this.setState({ cities: JSON.parse(response.data), loading: false });
            }
        })
        .catch((error) => {
            alert(error + '\nMessage: ' + error.response.data.responseText);
            this.setState({ cities: null });
        });
    }

    async updateContries() {
        await axios.get(this.baseURL + 'GetCountries').then((response) => {
            if (response.status == 200) {
                this.setState({ countries: JSON.parse(response.data) });
            }
        })
        .catch((error) => {
            alert(error + '\nMessage: ' + error.response.data.responseText);
            this.setState({ countries: null });
        });
    }

    showCityModal = (city) => {
        this.updateContries();

        if (this.state.countries != null) {
            this.setState({ city: city, modalShow: true });
        }
    }

    hideCityModal = () => {
        this.setState({ city: null, modalShow: false });
    }

    async submitCity(e) {
        e.preventDefault();
        // same modal & form is used for edit and create, but modal.city is never set on create 
        let editing = isNumeric(e.target.elements['formCity'].value);

        axios.post(this.baseURL + (editing ? 'EditCity' : 'CreateCity'), null, {
            params: {
                cityId: e.target.elements['formCity'].value,
                cityName: e.target.elements['formName'].value,
                countryId: e.target.elements['formCountry'].value
            }
        }).then((response) => {
            this.updateCities();
            this.hideCityModal();
        })
        .catch(function (error) {
            alert(error + '\nMessage: ' + error.response.data.responseText);
        });
    }

    async removeCity(id) {
        axios.post(this.baseURL + 'RemoveCity', null, {
            params: {
                cityId: id
            }
        }).then((response) => {
            this.updateCities();
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
                    <h1 className="pb-3">Cities</h1>
                    <CitiesTable cities={this.state.cities}
                                    onCreateCityClick={this.showCityModal}
                                    onEditCityClick={this.showCityModal}
                                    onRemoveCityClick={this.removeCity}/>
                    
                    <CityModal city={this.state.city}
                        countries={this.state.countries}
                                  show={this.state.modalShow}
                                  onHide={this.hideCityModal}
                                  onSubmit={this.submitCity}/>
                </div>
            );
        }
    }
}

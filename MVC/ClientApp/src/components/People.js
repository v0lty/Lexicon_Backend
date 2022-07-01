import React, { Component, useEffect, useState } from 'react';
import axios from 'axios';
import { TailSpin } from 'react-loader-spinner'
import { PersonModal } from './PersonModal'
import { PeopleTable } from './PeopleTable'
import { isNumeric } from 'jquery';


export class PeopleComponent extends Component {

    baseURL = 'https://localhost:44395/api/';

    constructor(props) {
        super(props);
        this.state = { people: [], countries: [], person: null, loading: true, modalShow: false };
        this.submitPerson = this.submitPerson.bind(this);
        this.updatePeople = this.updatePeople.bind(this);
        this.updateContries = this.updateContries.bind(this);
        this.removePerson = this.removePerson.bind(this);
        this.showPersonModel = this.showPersonModel.bind(this);
        this.hidePersonModel = this.hidePersonModel.bind(this);
    }

    componentDidMount() {
        this.updatePeople();
        this.updateContries();
    }

    async updatePeople() {
        await axios.get(this.baseURL + 'GetPeople').then((response) => {
            if (response.status == 200) {
                this.setState({ people: JSON.parse(response.data), loading: false });                
            }
        })
        .catch((error) => {
            alert(error + '\nMessage: ' + error.response.data.responseText);
            this.setState({ people: null, loading: true });
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

    async submitPerson(e) {
        e.preventDefault();

        // same modal & form is used for edit and create, but modal.person is never set on create 
        let editing = isNumeric(e.target.elements['formPerson'].value);

        axios.post(this.baseURL + (editing ? 'EditPerson' : 'CreatePerson'), null, {
            params: {
                personId: e.target.elements['formPerson'].value,
                personName: e.target.elements['formName'].value,
                personPhone: e.target.elements['formPhone'].value,
                personCityId: e.target.elements['formCity'].value,
            }
        }).then((response) => {
            this.updatePeople();
            this.hidePersonModel();
        })
        .catch(function (error) {
            alert(error + '\nMessage: ' + error.response.data.responseText);
        });
    }

    async removePerson(id) {
        axios.post(this.baseURL + 'RemovePerson', null, {
            params: {
                personId: id
            }
        }).then((response) => {
            this.updatePeople();
        })
        .catch(function (error) {
            alert(error + '\nMessage: ' + error.response.data.responseText);
        });
    }

    showPersonModel = (person) => {
        this.setState({ person: person, modalShow: true });
    }

    hidePersonModel = () => {
        this.setState({ person: null, modalShow: false });
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
                    <h1 className="pb-3">People</h1>

                    <PeopleTable people={this.state.people}
                                 onCreatePersonClick={this.showPersonModel}
                                 onEditPersonClick={this.showPersonModel}
                                 onRemovePersonClick={this.removePerson}/>

                    <PersonModal person={this.state.person}
                                 show={this.state.modalShow}
                                 countries={this.state.countries}
                                 onHide={this.hidePersonModel}
                                 onSubmit={this.submitPerson}/>
                </div>
            );
        }
    }
}

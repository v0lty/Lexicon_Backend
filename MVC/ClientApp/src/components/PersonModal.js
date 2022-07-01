import React, { Component, useEffect, useState } from 'react';
import Modal from 'react-bootstrap/Modal'
import ModalDialog from 'react-bootstrap/ModalDialog'
import ModalHeader from 'react-bootstrap/ModalHeader'
import ModalTitle from 'react-bootstrap/ModalTitle'
import ModalBody from 'react-bootstrap/ModalBody'
import ModalFooter from 'react-bootstrap/ModalFooter'
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'

function getItemById(array, id) {
    return array?.find(item => { return item.Id === id; });
}

export function PersonModal(props) {
    const [selectedCountry, setSelectedCountry] = React.useState(null);
    const person = props?.person;
    return (        
        <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    {person != null ? "Edit Person" : "Create Person" }
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={() => props.onSubmit}>
                    <Form.Group controlId="formPerson">
                        <Form.Control type="text" className="d-none" defaultValue={person?.Id} />
                    </Form.Group>
                    <Form.Group className="mb-2" controlId="formName">
                        <Form.Label>Name</Form.Label>
                        <Form.Control type="text" defaultValue={person?.Name} required/>
                    </Form.Group>
                    <Form.Group className="mb-2" controlId="formPhone">
                        <Form.Label>Phone</Form.Label>
                        <Form.Control type="text" defaultValue={person?.Phone} required />
                    </Form.Group>
                    <Form.Group className="mb-2" controlId="formCountry">
                        <Form.Label>Country</Form.Label>
                        <Form.Select className="p-2 border border-secondary rounded w-100"
                            defaultValue={person != null ? person.CountryId : "Select a Country"}
                            onChange={(e) => setSelectedCountry(getItemById(props?.countries, parseInt(e.target.value)))} required>
                            <option disabled hidden>Select a Country</option>
                            {props.countries?.map((country, index) =>
                                <option key={country.Id} value={country.Id}>{country.Name}</option>
                            )}
                        </Form.Select>
                    </Form.Group>
                    <Form.Group className="mb-2" controlId="formCity">
                        <Form.Label>City</Form.Label>
                        <Form.Select className="p-2 border border-secondary rounded w-100" defaultValue={person?.CityId} required>
                            {(selectedCountry ?? getItemById(props?.countries, person?.CountryId))
                             ?.Cities?.map((city, index) =>
                                 <option key={city.Id} value={city.Id}>{city.Name}</option>
                            )}
                        </Form.Select>
                    </Form.Group>
                    <Button className="btn btn-primary mt-2" type="submit">Submit</Button>
                </Form>
            </Modal.Body>
        </Modal>
    );
}
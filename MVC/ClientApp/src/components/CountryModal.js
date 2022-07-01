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

export function CountryModal(props) {
    const country = props?.country;
    return (
        <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    {country != null ? "Edit Country" : "Create Country"}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={() => props.onSubmit}>
                    <Form.Group controlId="formCountry">
                        <Form.Control type="text" className="d-none" defaultValue={country?.Id} />
                    </Form.Group>
                    <Form.Group className="mb-2" controlId="formName">
                        <Form.Label>Name</Form.Label>
                        <Form.Control type="text" defaultValue={country?.Name} required />
                    </Form.Group>                   
                    <Button className="btn btn-primary mt-2" type="submit">Submit</Button>
                </Form>
            </Modal.Body>
        </Modal>
    );
}
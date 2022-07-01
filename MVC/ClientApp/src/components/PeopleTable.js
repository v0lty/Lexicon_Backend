import React, { Component, useEffect, useState } from 'react';
import sortHook, { SortButton } from './SortHook'

export const PeopleTable = props => {
    const { items, requestSort, sortConfig } = sortHook(
        props.people,
        props.config
    )
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th><SortButton sortConfig={sortConfig} id="Id" onClick={() => requestSort('Id')} /></th>
                    <th><SortButton sortConfig={sortConfig} id="Name" onClick={() => requestSort('Name')} /></th>
                    <th><SortButton sortConfig={sortConfig} id="Phone" onClick={() => requestSort('Phone')} /></th>
                    <th><SortButton sortConfig={sortConfig} id="City" onClick={() => requestSort('City')} /> </th>
                    <th><SortButton sortConfig={sortConfig} id="Country" onClick={() => requestSort('Country')} /> </th>
                    <th className="pb-3">Languages</th>
                    <th className="tiny-th">
                        <button className="btn btn-light text-info" id="add" onClick={() => props.onCreatePersonClick()}>
                            &#x271A; Add Person
                        </button>
                    </th>
                </tr>
            </thead>
            <tbody>
                {items.map(person =>
                    <tr key={person.Id}>
                        <td>{person.Id}</td>
                        <td>{person.Name}</td>
                        <td>{person.Phone}</td>
                        <td>{person.City}</td>
                        <td>{person.Country}</td>
                        <td>{person.Languages.map(language => language.Name).join(', ')}</td>
                        <td>
                            <button className="btn btn-light text-info py-0" id="edit" onClick={() => props.onEditPersonClick(person)}>
                                &#x2630;
                            </button>
                            <button className="btn btn-light text-danger py-0" id="delete" onClick={() => props.onRemovePersonClick(person.Id)}>
                                &#x2715;
                            </button>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}
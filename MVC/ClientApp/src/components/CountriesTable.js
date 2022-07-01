import React, { Component, useEffect, useState } from 'react';
import sortHook, { SortButton } from './SortHook'

export const CountriesTable = props => {
    const { items, requestSort, sortConfig } = sortHook(
        props.countries,
        props.config
    )
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th><SortButton sortConfig={sortConfig} id="Id" onClick={() => requestSort('Id')} /></th>
                    <th><SortButton sortConfig={sortConfig} id="Name" onClick={() => requestSort('Name')} /></th>
                    <th className="pb-3">Cities</th>
                    <th className="tiny-th">
                        <button className="btn btn-light text-info" id="add" onClick={() => props.onCreateCountryClick()}>
                            &#x271A; Add Country
                        </button>
                    </th>
                </tr>
            </thead>
            <tbody>
                {items.map(country =>
                    <tr key={country.Id}>
                        <td>{country.Id}</td>
                        <td>{country.Name}</td>
                        <td>{country.Cities.map(city => city.Name).join(', ')}</td>
                        <td>
                            <button className="btn btn-light text-info py-0" id="edit" onClick={() => props.onEditCountryClick(country)}>
                                &#x2630;
                            </button>
                            <button className="btn btn-light text-danger py-0" id="delete" onClick={() => props.onRemoveCountryClick(country.Id)}>
                                &#x2715;
                            </button>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}
import React, { Component, useEffect, useState } from 'react';
import sortHook, { SortButton } from './SortHook'

export const CitiesTable = props => {
    const { items, requestSort, sortConfig } = sortHook(
        props.cities,
        props.config
    )
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th><SortButton sortConfig={sortConfig} id="Id" onClick={() => requestSort('Id')} /></th>
                    <th><SortButton sortConfig={sortConfig} id="Name" onClick={() => requestSort('Name')} /></th>
                    <th className="pb-3">Country</th>
                    <th className="tiny-th">
                        <button className="btn btn-light text-info" id="add" onClick={() => props.onCreateCityClick()}>
                            &#x271A; Add City
                        </button>
                    </th>
                </tr>
            </thead>
            <tbody>
                {items.map(city =>
                    <tr key={city.Id}>
                        <td>{city.Id}</td>
                        <td>{city.Name}</td>
                        <td>{city.Country}</td>
                        <td>
                            <button className="btn btn-light text-info py-0" id="edit" onClick={() => props.onEditCityClick(city)}>
                                &#x2630;
                            </button>
                            <button className="btn btn-light text-danger py-0" id="delete" onClick={() => props.onRemoveCityClick(city.Id)}>
                                &#x2715;
                            </button>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}
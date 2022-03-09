import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './Licensing.scss';

export default class Licensing extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="licensing" />
                <div className="page-contents-container">
                    <p className="page-title">Licensing</p>
                </div>
            </div>
        )
    }
}

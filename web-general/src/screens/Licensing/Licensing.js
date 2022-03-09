import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './Licensing.scss';

import apache from '../../../assets/img/apache.png';

export default class Licensing extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="licensing" />
                <div className="page-contents-container">
                    <p className="page-title">Licensing</p>
                    <p className="text">In order to ensure the trust in our platform, all our downloadable softwares are licensed under the open-source license Apache License 2.0</p>
                    <div className="page-section">
                        <div className="license">
                            <div className="license-topbar">
                                <img src={apache} className="license-image" />
                                <p className="license-title">Apache License 2.0</p>
                            </div>
                            <a href="https://www.apache.org/licenses/LICENSE-2.0" target="_blank">
                                <button className="action-button">Learn more</button>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

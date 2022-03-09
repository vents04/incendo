import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './Organizations.scss';

export default class Organizations extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="organizations" />
                <div className="page-contents-container">
                    <p className="page-title">Organizations</p>
                </div>
            </div>
        )
    }
}

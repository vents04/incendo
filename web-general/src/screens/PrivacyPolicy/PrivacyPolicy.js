import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './PrivacyPolicy.scss';

export default class PrivacyPolicy extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="privacy-policy" />
                <div className="page-contents-container">
                    <p className="page-title">Privacy policy</p>
                </div>
            </div>
        )
    }
}

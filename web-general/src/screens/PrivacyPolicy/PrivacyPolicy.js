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
                    <p className="text">Incendo is a platform which relies on the publicity of all the data shared with us which is the main reason why nearly all of the data you have shared with us will be visible to other users.</p>
                    <div className="page-section">
                        <a href="https://resources.uploy.app/privacy-policy-incendo.html" target="_blank">
                            <button className="action-button">Open the privacy policy</button>
                        </a>
                    </div>
                </div>
            </div>
        )
    }
}

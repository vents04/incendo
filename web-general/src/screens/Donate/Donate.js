import React, { Component } from 'react'
import { Link } from 'react-router-dom';
import Navbar from '../../components/Navbar/Navbar';

import './Donate.scss';

export default class Donate extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="donate" />
                <div className="page-contents-container">
                    <p className="page-title">Donate</p>
                    <p className="text">As you probably know running a non-profit is challenging sometimes. That is why we are accepting donations. Every penny sent by you means a lot to us and our cause!</p>
                    <div className="page-section">
                        <a href="https://www.paypal.com/donate/?hosted_button_id=UXVR5AKNURUXL" target="_blank">
                            <button className="action-button">Make a donation</button>
                        </a>
                    </div>
                </div>
            </div>
        )
    }
}

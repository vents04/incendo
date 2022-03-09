import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './WhitePaper.scss';

export default class WhitePaper extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="white-paper" />
                <div className="page-contents-container">
                    <p className="page-title">White paper</p>
                </div>
            </div>
        )
    }
}

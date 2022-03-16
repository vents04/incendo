import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar'

export default class Home extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="home" />
                <div className="page-contents-container">
                    <p className="page-title">Home</p>
                    <p className="notation">This page is under development.</p>
                </div>
            </div>
        )
    }
}

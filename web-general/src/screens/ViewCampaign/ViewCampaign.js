import React, { Component } from 'react'
import { BiArrowBack } from 'react-icons/bi';
import { Link } from 'react-router-dom';
import Artifact from '../../components/Artifact/Artifact';
import Navbar from '../../components/Navbar/Navbar'

import "./ViewCampaign.scss";

export default class ViewCampaign extends Component {
    render() {
        return (
            <>
                <Navbar />
                <div className="topbar-container">
                    <Link to="/home">
                        <BiArrowBack style={{ cursor: 'pointer' }} />
                    </Link>
                    <p className="topbar-title">View campaign</p>
                </div>
                <div className="page-container">
                    <div className="page-section">
                        <p className="page-section-title">Artifacts</p>
                        <p className="notation">Artifacts contain signed information about the campaign</p>
                        <Artifact />
                    </div>
                </div>
            </>
        )
    }
}

import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './Download.scss';

import { AiFillExclamationCircle, AiFillEye } from 'react-icons/ai';
import { CgServer } from 'react-icons/cg';

export default class Download extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="download" />
                <div className="page-contents-container">
                    <p className="page-title">Download</p>
                    <p className="text">From this page you may download the latest versions of the self-hosted platforms of Incendo.</p>
                    <div className="important" style={{ marginTop: "2rem" }}>
                        <AiFillExclamationCircle style={{
                            color: "#fff",
                            marginRight: 8
                        }} size={20} />
                        <p>You can find the installation instructions in the downloaded archives</p>
                    </div>
                    <div className="page-section">
                        <p className="page-section-title">Organization server</p>
                        <div className="page-section-content">
                            <p className="text">The organization server is a self-hosted application which enables you to create and manage campaigns. It requires login/signup before you are able to use it.</p>
                            <div className="download-button">
                                <CgServer />
                                <p className="download-button-text">Download organization server</p>
                            </div>
                        </div>
                    </div>
                    <div className="page-section">
                        <p className="page-section-title">Watcher</p>
                        <div className="page-section-content">
                            <p className="text">The watcher is a self-hosted application which provides the capability to monitor multiple campaigns. You may check if there are problems related to them, their details and much more. All these features are available without authentication.</p>
                            <div className="download-button">
                                <AiFillEye />
                                <p className="download-button-text">Download watcher</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

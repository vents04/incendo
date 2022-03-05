import React, { Component } from 'react'

import './Home.scss';

import { VscWorkspaceTrusted } from 'react-icons/vsc'

export default class Home extends Component {
    render() {
        return (
            <>
                <div className="logo-container">
                    <VscWorkspaceTrusted className="logo" size={28} />
                    <p className="logo-title-navbar">Incendo watcher</p>
                </div>
                <div className="campaigns">
                    <div className="campaign">
                        <p className="campaign-status campaign-status-ok">No problems detected</p>
                        <p className="campaign-title">Campaign title</p>
                    </div>
                    <div className="campaign">
                        <p className="campaign-status campaign-status-not-ok">Problems detected</p>
                        <p className="campaign-title">Campaign title 2</p>
                    </div>
                </div>
            </>
        )
    }
}

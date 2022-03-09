import React, { Component } from 'react'
import { VscWorkspaceTrusted } from 'react-icons/vsc'

import './Topbar.scss';

export default class Topbar extends Component {
    render() {
        return (
            <div className="logo-container">
                <VscWorkspaceTrusted className="logo" size={28} />
                <p className="logo-title-navbar">Incendo watcher</p>
            </div>
        )
    }
}

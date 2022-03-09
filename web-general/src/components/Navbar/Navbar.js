import React, { Component } from 'react'

import './Navbar.scss';

import { VscWorkspaceTrusted } from 'react-icons/vsc'
import { IoMdClose, IoMdMenu } from 'react-icons/io';
import { Link } from 'react-router-dom';

export default class Navbar extends Component {

    state = {
        showMenu: false
    }

    render() {
        return (
            <>
                <div className="navbar">
                    <div className="navbar-logo-container">
                        <div>
                            <VscWorkspaceTrusted style={{
                                color: "#66d37e"
                            }} size={25} />
                            <p className="navbar-logo-title">Incendo</p>
                        </div>
                    </div>
                    <div className="navbar-menu">
                        <Link to="/home">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'home' ? 'Main Bold' : 'Main Regular'
                            }}>Home</p>
                        </Link>
                        <Link to="/white-paper">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'white-paper' ? 'Main Bold' : 'Main Regular'
                            }}>White paper</p>
                        </Link>
                        <Link to="/organizations">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'organizations' ? 'Main Bold' : 'Main Regular'
                            }}>Organizations</p>
                        </Link>
                        <Link to="/download">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'download' ? 'Main Bold' : 'Main Regular'
                            }}>Download</p>
                        </Link>
                        <Link to="/contacts">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'contacts' ? 'Main Bold' : 'Main Regular'
                            }}>Contacts</p>
                        </Link>
                        <Link to="/privacy-policy">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'privacy-policy' ? 'Main Bold' : 'Main Regular'
                            }}>Privacy policy</p>
                        </Link>
                        <Link to="/licensing">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'licensing' ? 'Main Bold' : 'Main Regular'
                            }}>Licensing</p>
                        </Link>
                        <Link to="/donate">
                            <p className="navbar-menu-item" style={{
                                fontFamily: this.props.activeMenu == 'donate' ? 'Main Bold' : 'Main Regular'
                            }}>Donate</p>
                        </Link>
                    </div>
                </div>
            </>
        )
    }
}

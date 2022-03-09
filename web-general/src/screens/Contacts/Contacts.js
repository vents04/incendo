import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './Contacts.scss';

import { MdEmail, MdPhone } from 'react-icons/md';

export default class Contacts extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="contacts" />
                <div className="page-contents-container">
                    <p className="page-title">Contacts</p>
                    <p className="text">You may contact the non-profit Incendo with whatever questions you have in mind regarding our platform logic and functionalities, as well as potential bugs you have discovered. Our team will reach out to you as soon as possible.</p>
                    <div className="page-section" style={{
                        width: 'max-content'
                    }}>
                        <a href="mailto:support@uploy.app" style={{
                            display: "flex",
                            flexDirection: "row",
                            justifyContent: "center"
                        }}>
                            <div className="contact-item">
                                <MdEmail size={28} />
                                <p className="contact-item-value">support@uploy.app</p>
                            </div>
                        </a>
                        <a href="tel:+359882764107" style={{
                            display: "flex",
                            flexDirection: "row",
                            justifyContent: "center"
                        }}>
                            <div className="contact-item">
                                <MdPhone size={28} />
                                <p className="contact-item-value">088 276 4107</p>
                            </div>
                        </a>
                    </div>
                </div>
            </div >
        )
    }
}

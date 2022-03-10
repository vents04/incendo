import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';

import './PrivacyPolicy.scss';

export default class PrivacyPolicy extends Component {
    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="privacy-policy" />
                <div className="page-contents-container" style={{
                    display: "flex",
                    flexDirection: "column",
                    paddingBottom: "1rem"
                }}>
                    <p className="page-title">Privacy policy</p>
                    <div className="page-section" style={{
                        flexGrow: 1,
                        flexShrink: 1,
                        overflowY: "auto"
                    }}>
                        <p className="text">This system has been created for the sole purpose of transparency. Because of this all the data except passwords will be publicly available to all users on the platforms of Incendo. The types of data that we collect is organization-related (organization name, description and other data collected to ensure platform transparency) and watcher-related (actions taken on them).</p>

                        <h2 className="page-section-title">Consent</h2>
                        <p className="text">By using our website, you hereby consent to our Privacy Policy and agree to its terms.</p>

                        <h2 className="page-section-title">Information we collect</h2>
                        <p className="text">The only types of data that we collect are as follows:</p>
                        <ul className="text">
                            <li>Organization data</li>
                            <li>Campaign data</li>
                            <li>Campaign modification data</li>
                        </ul>

                        <h2 className="page-section-title">How we use your information</h2>
                        <p className="text">We use the information we collect in various ways, including to:</p>
                        <ul className="text">
                            <li>Find and prevent fraud</li>
                            <li>Provide, operate, and maintain our website</li>
                            <li>Develop new products, services, features, and functionality</li>
                            <li>Communicate with you, either directly or through one of our partners, including for customer service, to provide you with updates and other information relating to the website</li>
                        </ul>

                        <h2 className="page-section-title">Log Files</h2>
                        <p className="text">Incendo follows a standard procedure of using log files. These files log visitors when they visit websites. All hosting companies do this and a part of hosting services' analytics. The information collected by log files include internet protocol (IP) addresses, browser type, Internet Service Provider (ISP), date and time stamp, referring/exit pages, and possibly the number of clicks. These are not linked to any information that is personally identifiable.</p>

                        <h2 className="page-section-title">Cookies</h2>
                        <p className="text">Like most websites, Incendo uses 'cookies'. The only type of cookies we use is authentication-related cookies which are used to identify your organization when taking actions on campaigns.</p>
                    </div>
                </div>
            </div>
        )
    }
}

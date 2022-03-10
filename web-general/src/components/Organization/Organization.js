import React, { Component } from 'react'
import { IoIosArrowDown, IoIosArrowUp } from 'react-icons/io';
import Campaign from '../Campaign/Campaign';

import './Organization.scss';

export default class Organization extends Component {

    state = {
        showCampaigns: true
    }

    render() {
        return (
            <div className="organization">
                <p className="organization-title">{this.props.organization.name}</p>
                <p className="organization-description">{this.props.organization.description}</p>
                {
                    this.props.organization.campaigns.length > 0
                        ? <>
                            <div className="organization-view-campaigns-toggle" onClick={() => {
                                this.setState({ showCampaigns: !this.state.showCampaigns });
                            }}>
                                <p className="organization-view-campaigns-toggle-title">View all 100 campaigns</p>
                                {
                                    !this.state.showCampaigns
                                        ? <IoIosArrowDown size={14} />
                                        : <IoIosArrowUp size={14} />
                                }
                            </div>
                            {
                                this.state.showCampaigns
                                && this.props.organization.campaigns.map((campaign) =>
                                    <Campaign campaign={campaign} />
                                )
                            }
                        </>
                        : <p className="text" style={{ marginTop: '8px', fontSize: '0.9rem', fontFamily: 'Main SemiBold' }}>This organization does not have any campaigns</p>
                }
            </div>
        )
    }
}

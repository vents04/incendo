import React, { Component } from 'react'

import './Campaign.scss'

import ApiRequests from '../../classes/ApiRequests'
import Auth from '../../classes/Auth'
import { Link } from 'react-router-dom'
import { MILLISECONDS_IN_A_MINUTE, CAMPAIGN_TYPES, CAMPAIGN_STATES } from '../../global'

export default class Campaign extends Component {
  render() {
    return (
        <div className="campaign-container">
            <Link to="/view-campaign/1">
                <div className="campaign">
                    <div className="campaign-topbar">
                        <p className="campaign-type">{this.props.campaign.type}</p>
                        <p className="highlighted-label" style={{
                            backgroundColor: this.props.campaign.state == CAMPAIGN_STATES.INACTIVE || this.props.campaign.state == CAMPAIGN_STATES.FINISHED
                            ? "#aaa" : this.props.campaign.state == CAMPAIGN_STATES.FAILED ? "red" : "#66d37e"
                            
                        }}>{this.props.campaign.state}</p>
                    </div>
                    <p className="campaign-name">{this.props.campaign.name}</p>
                    <p className="campaign-description">{this.props.campaign.description}</p>
                    <span className="campaign-setting">{(this.props.campaign.settings.modificationsPhaseDuration/MILLISECONDS_IN_A_MINUTE).toFixed(2)} minutes mod duration</span>
                    &nbsp;&middot;&nbsp;
                    <span className="campaign-setting">{(this.props.campaign.settings.decryptionPhaseDuration/MILLISECONDS_IN_A_MINUTE).toFixed(2)} minutes key duration</span>
                    {
                        this.props.campaign.state == CAMPAIGN_STATES.INACTIVE
                        && <div className='inline'>
                            <button className="action-button deny" onClick={() => {this.props.removeCampaign(this.props.campaign.id)}}>Remove campaign</button>
                            <p className="action-text" onClick={() => {this.props.activateCampaign(this.props.campaign.id)}}>Activate campaign</p>
                    </div>
                    }
                </div>
            </Link>
        </div>
    )
  }
}

import React, { Component } from 'react'
import { CAMPAIGN_STATES } from '../../global';

import './Campaign.scss';

export default class Campaign extends Component {
    render() {
        return (
            <div className="campaign">
                <div className="campaign-state campaign-state-neutral" style={{
                    backgroundColor:
                        this.props.campaign.state == CAMPAIGN_STATES.INACTIVE
                            || this.props.campaign.state == CAMPAIGN_STATES.FINISHED
                            ? "#ddd"
                            : this.props.campaign.state == CAMPAIGN_STATES.FAILED
                                ? "rgb(255, 0, 0)"
                                : "#66d37e"
                }}>{
                        this.props.campaign.state
                    }</div>
                <p className="campaign-name">{this.props.campaign.name}</p>
                <p className="campaign-description">{this.props.campaign.description}</p>
            </div>
        )
    }
}

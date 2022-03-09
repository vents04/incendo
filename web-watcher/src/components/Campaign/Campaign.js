import React, { Component } from 'react'

import './Campaign.scss';

import { IoIosArrowDown, IoIosArrowUp } from 'react-icons/io';
import { AiFillDelete } from 'react-icons/ai';

import Artifact from '../Artifact/Artifact';

export default class Campaign extends Component {

    state = {
        showArtifacts: false
    }

    render() {
        return (
            <div className="campaign">
                <div className="campaign-delete-container" onClick={() => {
                    this.props.deleteCampaign(this.props.campaign.id);
                }}>
                    <AiFillDelete size={20} />
                </div>
                <div className="campaign-topbar">
                    {
                        !this.props.campaign.hasProblems
                            ? <p className="campaign-topbar-item remove-padding-left campaign-status campaign-status-ok">No problems detected</p>
                            : <p className="campaign-topbar-item remove-padding-left campaign-status campaign-status-not-ok">Problems detected</p>
                    }
                    &middot;
                    <p className="campaign-topbar-item">
                        {
                            this.props.campaign.isUserParticipant
                                ? "You participate"
                                : "You do not participate"
                        }
                    </p>
                    &middot;
                    <p className="campaign-topbar-item">{this.props.campaign.state}</p>
                    &middot;
                    <p className="campaign-topbar-item">Phase: {this.props.campaign.phase}</p>
                </div>
                <p className="campaign-name">{this.props.campaign.name}</p>
                <p className="campaign-description">{this.props.campaign.description}</p>
                {
                    this.props.campaign.artifacts.length > 0
                        ? <>
                            <div className="campaign-view-more-toggle-container" onClick={() => {
                                this.setState({ showArtifacts: !this.state.showArtifacts })
                            }}>
                                <p className="campaign-view-more-toggle-title">View artifacts</p>
                                {
                                    !this.state.showArtifacts
                                        ? <IoIosArrowDown size={14} />
                                        : <IoIosArrowUp size={14} />
                                }
                            </div>
                            {
                                this.state.showArtifacts
                                && this.props.campaign.artifacts.map((artifact) =>
                                    <Artifact artifact={artifact} />
                                )
                            }
                        </>
                        : null
                }
            </div>
        )
    }
}

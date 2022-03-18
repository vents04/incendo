import React, { Component } from 'react'

import { GrClose } from 'react-icons/gr';
import ApiRequests from '../../classes/ApiRequests';
import { CAMPAIGN_TYPES, MILLISECONDS_IN_A_MINUTE } from '../../global';

import './AddCampaign.scss';

export default class AddCampaign extends Component {

    state = {
        name: "",
        description: "",
        type: CAMPAIGN_TYPES.SHUFFLE,
        modDuration: 0,
        keyDuration: 0,
        permutationLength: 0,
        showError: false,
        error: "",
        showLoading: false
    }

    addCampaign = () => {
        this.setState({showError: false, error: "", showLoading: true});
        const modDuration = this.state.modDuration * MILLISECONDS_IN_A_MINUTE;
        const keyDuration = this.state.keyDuration * MILLISECONDS_IN_A_MINUTE;
        ApiRequests.post("campaigns/", {}, {
            name: this.state.name,
            description: this.state.description,
            type: this.state.type,
            settings: {
                modificationsPhaseDuration: modDuration,
                decryptionPhaseDuration: keyDuration,
                permutationLength: this.state.permutationLength
            }
        }, true).then((response) => {
            this.setState({showLoading: false});
            this.props.toggleShowAddCampaignModal(false);
        }).catch((error) => {
            this.setState({showLoading: false});
            if (error.response) {
                this.setState({ error: error.response.data, showError: true });
            } else if (error.request) {
                this.setState({ showError: true, error: "Response not returned" });
            } else {
                this.setState({ showError: true, error: "Request setting error" });
            }
        })
    }

    render() {
        return (
            <div className="centered-content">
                <div className="modal-box">
                    <div className="modal-topbar">
                        <p className="modal-title">Add campaign</p>
                        <GrClose style={{ cursor: "pointer" }} onClick={() => {
                            this.props.toggleShowAddCampaignModal(false)
                        }} />
                    </div>
                    <div className="modal-content">
                        <div className="modal-input-container">
                            <p className="modal-input-hint">Name</p>
                            <input type="text" className="modal-input" onInput={(evt) => {
                                this.setState({ name: evt.target.value });
                            }} />
                        </div>
                        <div className="modal-input-container">
                            <p className="modal-input-hint">Description</p>
                            <textarea type="text" className="modal-input" onInput={(evt) => {
                                this.setState({ description: evt.target.value });
                            }}></textarea>
                        </div>
                        <div className="modal-input-container">
                            <p className="modal-input-hint">Type</p>
                            <select className="modal-input" onChange={(evt) => {
                                this.setState({ type: evt.target.value });
                            }}>
                                <option value={CAMPAIGN_TYPES.SELECT}>Select</option>
                                <option value={CAMPAIGN_TYPES.SHUFFLE}>Shuffle</option>
                                <option value={CAMPAIGN_TYPES.ASSIGN}>Assign</option>
                            </select>
                        </div>
                        <div className="modal-input-container">
                            <p className="modal-input-hint">Mod duration in minutes</p>
                            <input type="number" className="modal-input" onInput={(evt) => {
                                this.setState({ modDuration: evt.target.value });
                            }} />
                        </div>
                        <div className="modal-input-container">
                            <p className="modal-input-hint">Key duration in minutes</p>
                            <input type="number" className="modal-input" onInput={(evt) => {
                                this.setState({ keyDuration: evt.target.value });
                            }} />
                        </div>
                        <div className="modal-input-container">
                            <p className="modal-input-hint">Permutation length</p>
                            <input type="number" className="modal-input" onInput={(evt) => {
                                this.setState({ permutationLength: evt.target.value });
                            }} />
                        </div>
                        {this.state.showError && <p className="error-box">{this.state.error}</p>}
                        <button className="action-button-modal" onClick={this.addCampaign}>
                            {this.state.showLoading ? "Saving..." : "Submit"}
                        </button>
                    </div>
                </div>
            </div >
        )
    }
}

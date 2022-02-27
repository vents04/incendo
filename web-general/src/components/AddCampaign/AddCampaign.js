import React, { Component } from 'react'

import { GrClose } from 'react-icons/gr';

import './AddCampaign.scss';

export default class AddCampaign extends Component {

    state = {
        name: "",
        description: "",
        type: "SHUFFLE",
        modDuration: 0,
        keyDuration: 0,
        showError: true,
        error: "test error"
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
                                <option value="SHUFFLE">Shuffle</option>
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
                        {this.state.showError && <p className="error-box">{this.state.error}</p>}
                        <button className="action-button-modal">Submit</button>
                    </div>
                </div>
            </div >
        )
    }
}

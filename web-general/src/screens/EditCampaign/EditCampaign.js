import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar'

import "./EditCampaign.scss";
import { BiArrowBack } from 'react-icons/bi';
import { Link } from 'react-router-dom';

export default class EditCampaign extends Component {

    state = {
        editable: false,
        name: "",
        description: "",
        type: "SHUFFLE",
        modDuration: 0,
        keyDuration: 0
    }

    update = () => {

    }

    render() {
        return (
            <>
                <Navbar showAuthButtons={false} />
                <div className="topbar-container">
                    <Link to="/home">
                        <BiArrowBack style={{ cursor: 'pointer' }} />
                    </Link>
                    <p className="topbar-title">Edit campaign</p>
                </div>
                <div className="page-container">
                    {
                        !this.state.editable && <p className="disabled-message">
                            This campaign has already been activated which means that you will not be able to edit it furthermore
                        </p>
                    }
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Name</p>
                        <input type="text" className="modal-input" onInput={(evt) => {
                            this.setState({ name: evt.target.value });
                        }} disabled={!this.state.editable} />
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Description</p>
                        <textarea type="text" className="modal-input" onInput={(evt) => {
                            this.setState({ description: evt.target.value });
                        }} disabled={!this.state.editable}></textarea>
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Type</p>
                        <select className="modal-input" onChange={(evt) => {
                            this.setState({ type: evt.target.value });
                        }} disabled={!this.state.editable}>
                            <option value="SHUFFLE">Shuffle</option>
                        </select>
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Mod duration in minutes</p>
                        <input type="number" className="modal-input" onInput={(evt) => {
                            this.setState({ modDuration: evt.target.value });
                        }} disabled={!this.state.editable} />
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Key duration in minutes</p>
                        <input type="number" className="modal-input" onInput={(evt) => {
                            this.setState({ keyDuration: evt.target.value });
                        }} disabled={!this.state.editable} />
                    </div>
                    {this.state.showError && <p className="error-box">{this.state.error}</p>}
                    <button className="action-button-modal" onClick={this.addCampaign}
                        disabled={!this.state.editable} onClick={this.update}>Update</button>
                </div>
            </>
        )
    }
}

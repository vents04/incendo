import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar'

import "./EditCampaign.scss";
import { BiArrowBack } from 'react-icons/bi';
import { Link } from 'react-router-dom';
import Papa from 'papaparse';

export default class EditCampaign extends Component {

    state = {
        editable: true,
        name: "",
        description: "",
        type: "SHUFFLE",
        modDuration: 0,
        keyDuration: 0,
        campaignUrl: "http://neshtosi.com",
        csvFileSrc: null,
        csvFileValue: null
    }

    componentDidMount() {

    }

    update = () => {

    }

    onFileChange = e => {
        if (window.FileReader) {
            let file = e.target.files[0], reader = new FileReader(), self = this;
            Papa.parse(e.target.files[0], {
                complete: function (results) {
                    self.setState({ csvFileSrc: results.data }, () => {
                        e.target.value = "";
                    });
                }
            });
        }
        else {
            alert('Sorry, your browser does\'nt support for preview');
        }
    };

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
                    <div className="muodal-input-container">
                        <p className="modal-input-hint">Name</p>
                        <input type="text" className="modal-input" onInput={(evt) => {
                            this.setState({ name: evt.target.value });
                        }} readOnly={!this.state.editable} />
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Description</p>
                        <textarea type="text" className="modal-input" onInput={(evt) => {
                            this.setState({ description: evt.target.value });
                        }} readOnly={!this.state.editable}></textarea>
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Type</p>
                        <select className="modal-input" onChange={(evt) => {
                            this.setState({ type: evt.target.value });
                        }} readOnly={!this.state.editable}>
                            <option value="SHUFFLE">Shuffle</option>
                        </select>
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Mod duration in minutes</p>
                        <input type="number" className="modal-input" onInput={(evt) => {
                            this.setState({ modDuration: evt.target.value });
                        }} readOnly={!this.state.editable} />
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Key duration in minutes</p>
                        <input type="number" className="modal-input" onInput={(evt) => {
                            this.setState({ keyDuration: evt.target.value });
                        }} readOnly={!this.state.editable} />
                    </div>
                    <div className="modal-input-container">
                        <p className="modal-input-hint">Campaign URL</p>
                        <input type="text" className="modal-input" value={this.state.campaignUrl} readOnly={true} />
                    </div>
                    {
                        this.state.type == "SHUFFLE"
                            ? <div className="modal-input-container">
                                <p className="modal-input-hint">Upload shuffle list from CSV</p>
                                <p className="modal-input-notation">Your .csv file should contain two columns (id, displayName). <br />Please, use commas as a separator and also make sure to use double quotes when needed!</p>
                                <input type="file" accept="text/csv" multiple={false} readOnly={!this.state.editable} onChange={this.onFileChange} />
                            </div>
                            : null
                    }
                    <table>
                        {
                            this.state.csvFileSrc &&
                            this.state.csvFileSrc.map((row, index) =>
                                index == 0
                                    ? <thead>
                                        <tr>
                                            {
                                                row.map((col) =>
                                                    <th>{col}</th>
                                                )
                                            }
                                        </tr>
                                    </thead>
                                    : <tr>
                                        {
                                            row.map((col) =>
                                                <th>{col}</th>
                                            )
                                        }
                                    </tr>
                            )
                        }
                    </table>
                    {this.state.showError && <p className="error-box">{this.state.error}</p>}
                    <button className="action-button-modal" onClick={this.addCampaign}
                        readOnly={!this.state.editable} onClick={this.update}>Update</button>
                </div>
            </>
        )
    }
}

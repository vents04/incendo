import React, { Component } from 'react'
import { IoMdClose } from 'react-icons/io';

import './AddCampaignModal.scss';

export default class AddCampaignModal extends Component {

    state = {
        showError: false,
        error: ""
    }

    render() {
        return (
            <div className="modal-background">
                <div className="modal-container">
                    <div className="modal-header">
                        <p className="modal-title">Add campaign to watch</p>
                        <IoMdClose onClick={() => {
                            this.props.toggleShowAddCampaignModal(false)
                        }} style={{ cursor: 'pointer' }} />
                    </div>
                    <div className="modal-body">
                        <p className="modal-notation">Type in a campaign public URL to start monitoring it.</p>
                        <input type="text" className="modal-input" placeholder="Public campaign URL..." style={{
                            marginTop: '8px'
                        }} />
                    </div>
                    {this.state.showError && <p className="error-box" style={{ marginBottom: '16px' }}>{this.state.error}</p>}
                    <div className="modal-footer">
                        <button className="action-button" style={{
                            width: '100%'
                        }} onClick={() => {
                            this.props.toggleShowAddCampaignModal(false);
                        }}>Start watching</button>
                    </div>
                </div>
            </div >
        )
    }
}

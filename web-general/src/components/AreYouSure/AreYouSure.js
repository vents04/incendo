import React, { Component } from 'react'

import './AreYouSure.scss';

export default class AreYouSure extends Component {

    proceed = () => {
        switch (this.props.intention) {
            case "remove":
                this.props.removeCampaign();
                break;
            case "activate":
                this.props.activateCampaign();
                break;
        }
        this.props.toggleShowAreYouSureModal(false);

    }

    render() {
        return (
            <div className="centered-content">
                <div className="modal-box">
                    <div className="modal-topbar">
                        <p className="modal-title">Are you sure?</p>
                    </div>
                    <div className="modal-content">
                        <div className="options-container">
                            <p className="modal-option highlighted-text" onClick={this.proceed}>Yes</p>
                            <p className="modal-option" onClick={() => { this.props.toggleShowAreYouSureModal(false) }}>No</p>
                        </div>
                    </div>
                </div>
            </div >
        )
    }
}

import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar'

import './Home.scss'
import { GrAdd } from 'react-icons/gr'
import AddCampaign from '../../components/AddCampaign/AddCampaign'

export default class Home extends Component {

  state = {
    showAddCampaignModal: false,
  }

  addCampaign = () => {
    this.setState({ showAddCampaignModal: true });
  }

  toggleShowAddCampaignModal = (state) => {
    this.setState({ showAddCampaignModal: state });
  }

  render() {
    return (
      <div>
        <Navbar showAuthButtons={false} />
        {this.state.showAddCampaignModal && <AddCampaign toggleShowAddCampaignModal={this.toggleShowAddCampaignModal} />}
        <div className="page-container">
          <button className="action-button" onClick={this.addCampaign}>
            Add campaign
          </button>
          <div className="section-container">
            <p className="section-title">My campaigns</p>
            <div className="section-content">
              <div className="campaigns">
                <div className="campaign-container">
                  <div className="campaign">
                    <div className="campaign-topbar">
                      <p className="campaign-type">Shuffle</p>
                      <p className="highlighted-label">Activated</p>
                    </div>
                    <p className="campaign-name">Campaign name</p>
                    <p className="campaign-description">Campaign description dddddddddddddddddddddddddddd sad asdaskkd jasndlj basljkdbnaskbdl bashdbvlqewbdlbas doas hdas sdi asbbdasb dlas dnasli dasibd lhas</p>
                    <span className="campaign-setting">2h25m mod duration</span>
                    &nbsp;&middot;&nbsp;
                    <span className="campaign-setting">2h25m key duration</span>
                  </div>
                </div>
                <div className="campaign-container">
                  <div className="campaign">
                    <div className="campaign-topbar">
                      <p className="campaign-type">Shuffle</p>
                      <p className="campaign-action-text">Edit</p>
                    </div>
                    <p className="campaign-name">Campaign name</p>
                    <p className="campaign-description">Campaign description</p>
                  </div>
                </div>
                <div className="campaign-container">
                  <div className="campaign">
                    <div className="campaign-topbar">
                      <p className="campaign-type">Shuffle</p>
                      <p className="campaign-action-text">Edit</p>
                    </div>
                    <p className="campaign-name">Campaign name</p>
                    <p className="campaign-description">Campaign description</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    )
  }
}

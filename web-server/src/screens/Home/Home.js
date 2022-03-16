import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar'

import './Home.scss'
import AddCampaign from '../../components/AddCampaign/AddCampaign'
import { Link } from 'react-router-dom'
import AreYouSure from '../../components/AreYouSure/AreYouSure'

export default class Home extends Component {

  state = {
    campaigns: [{
      name: "test",
      show: true,
    }, {
      name: "2",
      show: true,
    }, {
      name: "testing",
      show: true,
    }],
    showAddCampaignModal: false,
    showAreYouSureModal: false,
    intention: "",
    intentionId: ""
  }

  addCampaign = () => {
    this.setState({ showAddCampaignModal: true });
  }

  removeCampaign = (id) => {

  }

  activateCampaign = (id) => {

  }

  toggleShowAddCampaignModal = (state) => {
    this.setState({ showAddCampaignModal: state });
  }

  toggleShowAreYouSureModal = (state) => {
    this.setState({ showAreYouSureModal: state });
  }

  search = (evt) => {
    const query = evt.target.value;
    let campaigns = this.state.campaigns;
    for (let campaign of campaigns) {
      campaign.show = (campaign.name.toLowerCase().trim().includes(query.toLowerCase().trim()));
    }
    this.setState({ campaigns: campaigns });
  }

  render() {
    return (
      <div>
        <Navbar showAuthButtons={true} />
        {this.state.showAddCampaignModal && <AddCampaign toggleShowAddCampaignModal={this.toggleShowAddCampaignModal} />}
        {this.state.showAreYouSureModal && <AreYouSure
          intention={this.state.intention}
          toggleShowAreYouSureModal={this.toggleShowAreYouSureModal}
          removeCampaign={this.removeCampaign(this.state.intentionId)}
          activateCampaign={this.activateCampaign(this.state.intentionId)} />}
        <div className="page-container">
          <button className="action-button" onClick={this.addCampaign}>
            Add campaign
          </button>
          <div className="section-container">
            <p className="section-title">My campaigns</p>
            <input type="text" className="search" placeholder="Search campaigns..." onInput={this.search}></input>
            <div className="section-content">
              <div className="campaigns">
                <div className="campaign-container">
                  <Link to="/view-campaign/1">
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
                  </Link>
                </div>
                <div className="campaign-container">
                  <div className="campaign">
                    <div className="campaign-topbar">
                      <p className="campaign-type">Shuffle</p>
                      <Link to="/edit-campaign/1">
                        <p className="campaign-action-text">Edit</p>
                      </Link>
                    </div>
                    <p className="campaign-name">Campaign name</p>
                    <p className="campaign-description">Campaign description</p>
                    <button className="action-button deny" onClick={() => {
                      this.setState({
                        intention: "remove",
                        intentionId: "1",
                        showAreYouSureModal: true,
                      })
                    }}>Remove campaign</button>
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
                    <p className="action-text" onClick={() => {
                      this.setState({
                        intention: "activate",
                        intentionId: "1",
                        showAreYouSureModal: true,
                      })
                    }}>Activate campaign</p>
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

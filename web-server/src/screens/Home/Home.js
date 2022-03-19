import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar'

import './Home.scss'
import AddCampaign from '../../components/AddCampaign/AddCampaign'
import { Link } from 'react-router-dom'
import AreYouSure from '../../components/AreYouSure/AreYouSure'
import Campaign from '../../components/Campaign/Campaign'
import ApiRequests from '../../classes/ApiRequests'

export default class Home extends Component {

  state = {
    campaigns: [
      {
        id: "1",
      name: "Campaign name",
      description: "Camapaign description",
      type: "Shuffle",
      settings: {
        modificationsPhaseDuration: 84000,
        decryptionPhaseDuration: 96000,
        permutationLength: 3
      },
      state: "Active"
    }, {
      id: "2",
      name: "Campaign name 2",
      description: "Camapaign description 2",
      type: "Assign",
      settings: {
        modificationsPhaseDuration: 84000,
        decryptionPhaseDuration: 96000,
        permutationLength: 3
      },
      state: "Inactive"
    }],
    showAddCampaignModal: false,
    showAreYouSureModal: false,
    intention: "",
    intentionId: "",
    showError: false,
    error: ""
  }

  componentDidMount() {
    this.getCampaigns();
  }

  getCampaigns = () => {
    ApiRequests.get("campaigns", {}, true).then((response) => {
      this.setState({campaigns: response.data.campaigns});
    }).catch((error) => {
      if (error.response) {
          this.setState({ error: error.response.data, showError: true });
      } else if (error.request) {
          this.setState({ showError: true, error: "Could not get campaigns because response was not returned" });
      } else {
          this.setState({ showError: true, error: "Could not get campaigns because there was a request setting error" });
      }
    })
  }

  addCampaign = () => {
    this.setState({ showAddCampaignModal: true });
  }

  removeCampaign = (id) => {
    ApiRequests.delete(`campaigns/${id}`, {}, true).then((response) => {
      this.getCampaigns();
    }).catch((error) => {
      if (error.response) {
          this.setState({ error: error.response.data, showError: true });
      } else if (error.request) {
          this.setState({ showError: true, error: "Response not returned" });
      } else {
          this.setState({ showError: true, error: "Request setting error" });
      }
    })
  }

  activateCampaign = (id) => {
    ApiRequests.put(`campaigns/${id}/activate`, {}, true).then((response) => {
      this.getCampaigns();
    }).catch((error) => {
      if (error.response) {
          this.setState({ error: error.response.data, showError: true });
      } else if (error.request) {
          this.setState({ showError: true, error: "Response not returned" });
      } else {
          this.setState({ showError: true, error: "Request setting error" });
      }
    })
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
        {this.state.showAddCampaignModal && <AddCampaign toggleShowAddCampaignModal={this.toggleShowAddCampaignModal} removeCampaign={this.removeCampaign} activateCampaign={this.activateCampaign} />}
        {this.state.showAreYouSureModal && <AreYouSure
          intention={this.state.intention}
          toggleShowAreYouSureModal={this.toggleShowAreYouSureModal}
          removeCampaign={this.removeCampaign(this.state.intentionId)}
          activateCampaign={this.activateCampaign(this.state.intentionId)} />}
        <div className="page-container">
        {this.state.showError && <p className="error-box">{this.state.error}</p>}
          <button className="action-button" onClick={this.addCampaign}>
            Add campaign
          </button>
          <div className="section-container">
            <p className="section-title">My campaigns</p>
            <input type="text" className="search" placeholder="Search campaigns..." onInput={this.search}></input>
            <div className="section-content">
              <div className="campaigns">
              {
                this.state.campaigns.map(campaign => 
                  <Campaign campaign={campaign}/>
                  )
                }
              </div>
            </div>
          </div>
        </div>
      </div>
    )
  }
}

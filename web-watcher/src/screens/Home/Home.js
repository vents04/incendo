import React, { Component } from 'react'

import './Home.scss';

import { VscWorkspaceTrusted } from 'react-icons/vsc'
import Topbar from '../../components/Topbar/Topbar';
import Campaign from '../../components/Campaign/Campaign';
import AddCampaignModal from '../../components/AddCampaignModal/AddCampaignModal';

export default class Home extends Component {

    state = {
        campaigns: [
            {
                id: 1,
                name: "National lottery",
                description: "This is a campaign about the generation of a permutation which will determine who is the person to win 1M dollars",
                type: "SHUFFLE",
                modDuration: 123124123,
                keyDuration: 123124123,
                campaignUrl: "https://incendo.com/campaign/national-lottery-1",
                artifacts: [
                    {
                        artifactType: "ErrorArtifact",
                        issueTime: 1646475257550,
                        challenge: "rebqwgerihqwkjr",
                        signaturePublicKey: "jqj82103i12jd12e;ld]21-3=210dj21md-21-91092umdpo21-03i12-0",
                        hash: "jikqwjhe102jeoi1283i12-0kdoi;h21sd2kjdoih2198dj12moidh21odo12d21d21d",
                        signature: "jkqwkjeiuwqh321u93i12-3=12dp21kdp2n08i312ndh1kdu1un903j21m3o21b81293joi",
                        errorCode: 404,
                        errorMessage: "Not found",
                        viewMore: false
                    },
                    {
                        artifactType: "OrganizationArtifact",
                        issueTime: 1646475257550,
                        challenge: "rebqwgerihqwkjr",
                        signaturePublicKey: "jqj82103i12jd12e;ld]21-3=210dj21md-21-91092umdpo21-03i12-0",
                        hash: "jikqwjhe102jeoi1283i12-0kdoi;h21sd2kjdoih2198dj12moidh21odo12d21d21d",
                        signature: "jkqwkjeiuwqh321u93i12-3=12dp21kdp2n08i312ndh1kdu1un903j21m3o21b81293joi",
                        organizationId: "idito na organizaciqta",
                        name: "imeto na organizaciqta",
                        publicKey: "qwdjojw3oi21oj312ibdo21",
                        viewMore: true
                    }
                ],
                hasProblems: false,
                isUserParticipant: false,
                state: "ACTIVE",
                phase: 1
            }
        ],
        showError: false,
        error: "",
        showAddCampaignModal: false
    }

    componentDidMount() {
        this.getCampaigns();
    }

    getCampaigns = () => {
        /*
        ApiRequests.get(``, {}).then((response) => {
            if (response.data.campaigns) this.setState({ campaigns: response.data.campaigns });
        }).catch((error) => {
            this.setState({
                showError: true,
                error: error.response.status != HTTP_STATUS_CODES.INTERNAL_SERVER_ERROR
                    ? error.response.data
                    : "Internal server error"
            });
        });
        */
    }

    deleteCampaign = (campaignId) => {
        console.log("eher")
        const campaigns = this.state.campaigns;
        for (let index = 0; index < campaigns.length; index++) {
            if (campaigns[index].id.toString() == campaignId.toString()) {
                campaigns.splice(index, 1);
                index--;
            }
        }
        this.setState({ campaigns: campaigns });
    }

    toggleShowAddCampaignModal = (state) => {
        this.setState({ showAddCampaignModal: state });
    }

    render() {
        return (
            <>
                {this.state.showAddCampaignModal && <AddCampaignModal toggleShowAddCampaignModal={this.toggleShowAddCampaignModal} />}
                <Topbar />
                <div className="page-container">
                    {
                        this.state.showError && <p className="error-box" style={{ marginBottom: '16px' }}>{this.state.error}</p>
                    }
                    <button className="action-button" onClick={() => {
                        this.setState({ showAddCampaignModal: true })
                    }} style={{
                        marginBottom: '16px'
                    }}>Add campaign to watch</button>
                    {
                        this.state.campaigns.length > 0
                            ? <div className="campaigns">
                                {
                                    this.state.campaigns.map((campaign) =>
                                        <Campaign campaign={campaign} deleteCampaign={this.deleteCampaign} />
                                    )
                                }
                            </div>
                            : <p className="notation">No campaigns to watch</p>
                    }
                </div>
            </>
        )
    }
}

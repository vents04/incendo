import React, { Component } from 'react'
import { IoIosArrowDown, IoIosArrowUp } from 'react-icons/io';

import './Artifact.scss';

export default class Artifact extends Component {

    state = {
        showMore: false
    }

    genericArtifactFields = ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature"];

    artifactTypesFields = {
        "ErrorArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "errorCode", "errorMessage"],
        "OrganizationArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "organizationId", "name", "publicKey"],
        "CampaignArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "organizationPublicKey", "campaignId", "campaignPublicKey", "configType", "configJson", "activationTime", "campaignHash"],
        "CampaignStateArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "campaignId", "campaignHash", "state", "phase", "stateTime", "eventCount", "permutationEncrypted"],
        "CampaignEventArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "campaignId", "campaignHash", "eventIndex", "eventType", "eventJson"],
        "ModArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "organizationPublicKey", "campaignId", "campaignHash", "participantPublicKey", "phase", "permutationEncrypted"],
        "ModConfirmationArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "organizationPublicKey", "campaignId", "campaignHash", "participantPublicKey", "phase", "permutationEncrypted", "recordTime", "index"],
        "ModKeyArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "organizationPublicKey", "campaignId", "campaignHash", "participantPublicKey", "phase", "permutationEncrypted", "permutationKey"],
        "ModKeyConfirmationArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "organizationPublicKey", "campaignId", "campaignHash", "participantPublicKey", "phase", "permutationEncrypted", "permutationKey", "keyRecordTime"],
        "CampaignOutcomeArtifact": ["artifactType", "issueTime", "challenge", "signaturePublicKey", "hash", "signature", "permutationOutcome", "modOutcome", "sequence", "outcomeType", "outcomeJson", "finishTime"]
    }

    artifactTypesTitles = {
        "ErrorArtifact": "Error",
        "OrganizationArtifact": "Organization identification",
        "CampaignArtifact": "Campaign identification",
        "CampaignStateArtifact": "Campaign state",
        "CampaignEventArtifact": "Campaign event",
        "ModArtifact": "Participation",
        "ModConfirmationArtifact": "Participation confirmation by the campaign",
        "ModKeyArtifact": "Participation AES key",
        "ModKeyConfirmationArtifact": "Participation key confirmation by the campaign",
        "CampaignOutcomeArtifact": "Campaign outcome"
    }

    render() {
        return (
            <div className="artifact">
                <p className="artifact-title">{this.artifactTypesTitles[this.props.artifact.artifactType]}</p>
                {
                    this.artifactTypesFields[this.props.artifact.artifactType].map((field) =>
                        !this.genericArtifactFields.includes(field)
                            ? <div className="artifact-field">
                                <p className="artifact-field-value">{field} : {this.props.artifact[field]}</p>
                            </div>
                            : null
                    )
                }
                <br />
                <p className="artifact-view-more" onClick={() => {
                    this.setState({ showMore: !this.state.showMore })
                }} >View more {
                        !this.state.showMore
                            ? <IoIosArrowDown size={14} style={{ marginLeft: 6 }} />
                            : <IoIosArrowUp size={14} style={{ marginLeft: 6 }} />
                    } </p>
                {
                    this.state.showMore &&
                    this.artifactTypesFields[this.props.artifact.artifactType].map((field) =>
                        this.genericArtifactFields.includes(field)
                            ? <div className="artifact-field">
                                <p className="artifact-field-value">{field} : {this.props.artifact[field]}</p>
                            </div>
                            : null
                    )
                }
            </div>
        )
    }
}

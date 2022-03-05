import React, { Component } from 'react'

import './Artifact.scss';

import { MdOutlineKeyboardArrowDown, MdOutlineKeyboardArrowUp } from 'react-icons/md';

export default class Artifact extends Component {

    state = {
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
            <>
                {
                    this.state.artifacts.map((artifact, index) =>
                        <div className="artifact">
                            <p className="artifact-title">{this.artifactTypesTitles[artifact.artifactType]}</p>
                            {
                                this.artifactTypesFields[artifact.artifactType].map((field) =>
                                    !this.genericArtifactFields.includes(field)
                                        ? <div className="artifact-field">
                                            <p className="artifact-field-value">{field} : {artifact[field]}</p>
                                        </div>
                                        : null
                                )
                            }
                            <br />
                            <p className="artifact-view-more" onClick={() => {
                                this.setState((state) => {
                                    let newState = { ...state, artifacts: [...state.artifacts] };
                                    newState.artifacts[index] = { ...newState.artifacts[index], viewMore: !newState.artifacts[index].viewMore };
                                    return newState;
                                });
                            }} >View more {
                                    !artifact.viewMore
                                        ? <MdOutlineKeyboardArrowDown />
                                        : <MdOutlineKeyboardArrowUp />
                                } </p>
                            {
                                artifact.viewMore &&
                                this.artifactTypesFields[artifact.artifactType].map((field) =>
                                    this.genericArtifactFields.includes(field)
                                        ? <div className="artifact-field">
                                            <p className="artifact-field-value">{field} : {artifact[field]}</p>
                                        </div>
                                        : null
                                )
                            }
                        </div>
                    )
                }
            </>
        )
    }
}

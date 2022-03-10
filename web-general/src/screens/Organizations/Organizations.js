import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar';
import Organization from '../../components/Organization/Organization';

import './Organizations.scss';

export default class Organizations extends Component {

    state = {
        organizations: [
            {
                name: "Sport toto",
                description: "Sport toto is an organization using Incendo for an honest choice in our betting games",
                campaigns: [
                    {
                        name: "National lottery",
                        description: "This is a campaign about the generation of a permutation which will determine who is the person to win 1M dollars",
                        state: "ACTIVE"
                    },
                    {
                        name: "National lottery",
                        description: "This is a campaign about the generation of a permutation which will determine who is the person to win 1M dollars",
                        state: "INACTIVE"
                    },
                    {
                        name: "National lottery",
                        description: "This is a campaign about the generation of a permutation which will determine who is the person to win 1M dollars",
                        state: "SEALED"
                    },
                    {
                        name: "National lottery",
                        description: "This is a campaign about the generation of a permutation which will determine who is the person to win 1M dollars",
                        state: "FINISHED"
                    },
                    {
                        name: "National lottery",
                        description: "This is a campaign about the generation of a permutation which will determine who is the person to win 1M dollars",
                        state: "FAILED"
                    }
                ]
            },
            {
                name: "Superior court",
                description: "Superior court uses Incendo for the process of distributing cases in the court",
                campaigns: []
            }
        ],
    }

    render() {
        return (
            <div className="page-container">
                <Navbar activeMenu="organizations" />
                <div className="page-contents-container" style={{
                    display: "flex",
                    flexDirection: "column",
                    paddingBottom: "1rem"
                }}>
                    <p className="page-title">Organizations</p>
                    <p className="text">On this page you can view all of the organizations with Incendo profiles. A list of their campaigns can also be found.</p>
                    <div className="page-section" style={{
                        flexGrow: 1,
                        flexShrink: 1,
                        overflowY: "auto"
                    }}>
                        {
                            this.state.organizations.map((organization) =>
                                <Organization organization={organization} />
                            )
                        }
                    </div>
                </div>
            </div>
        )
    }
}

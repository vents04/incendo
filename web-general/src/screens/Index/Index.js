import React, { Component } from 'react'
import Navbar from '../../components/Navbar/Navbar'

import './Index.scss'

export default class Index extends Component {
    render() {
        return (
            <Navbar showAuthButtons={true} />
        )
    }
}

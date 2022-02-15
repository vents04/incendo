import React, { Component } from 'react'

import './Navbar.css';

import { VscWorkspaceTrusted } from 'react-icons/vsc'

export default class Navbar extends Component {
  render() {
    return (
      <div className="navbar-container">
        <div className="logo-container">
          <VscWorkspaceTrusted  className="logo" size={20}/>
          <p className="logo-title">Incendo</p>
        </div>
        <div className="navbar-menu">
          <button className="navbar-button login-button">Log in</button>
          <button className="navbar-button signup-button">Sign up</button>
        </div>
      </div>
    )
  }
}

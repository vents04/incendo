import React, { Component } from 'react'

import { Link } from 'react-router-dom';

import './Navbar.scss';

import { VscWorkspaceTrusted } from 'react-icons/vsc'

export default class Navbar extends Component {

  componentDidMount() {

  }

  render() {
    return (
      <div className="navbar-container">
        <div className="logo-container">
          <VscWorkspaceTrusted className="logo" size={20} />
          <p className="logo-title-navbar">Incendo</p>
        </div>
        <div className="navbar-menu">
          {
            this.props.showAuthButtons
            && <>
              <Link to="/login">
                <button className="navbar-button">Log in</button>
              </Link>
              <Link to="/signup">
                <button className="navbar-button">Sign up</button>
              </Link>
            </>
          }
        </div>
      </div>
    )
  }
}

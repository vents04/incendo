import React, { Component } from 'react'

import './Login.css'

import { VscWorkspaceTrusted } from 'react-icons/vsc'

export default class Login extends Component {
  render() {
    return (
      <div className="centered-content">
        <div className="modal-box">
          <div className="modal-topbar">
            <div className="modal-topbar-left">
              <div className="logo-container">
                <VscWorkspaceTrusted  className="logo" size={20}/>
                <p className="logo-title">Incendo</p>
              </div>
              <p className="page-title">Login</p>
            </div>
          </div>
        </div>
      </div>
    )
  }
}

import React, { Component } from 'react'

import './Signup.scss'

import { VscWorkspaceTrusted } from 'react-icons/vsc'
import { Link, Navigate } from 'react-router-dom'

export default class Signup extends Component {

  state = {
    name: "",
    email: "",
    password: "",
    navigateToHome: false,
    showError: false,
    error: ""
  }

  signup = () => {
    if (this.state.name.length == 0 || this.state.email.length == 0 || this.state.password.length == 0) {
      this.setState({ showError: true, error: "Please fill in all the fields" });
      return;
    }
    this.setState({ navigateToHome: true });
  }

  render() {
    return (
      <div className="centered-content">
        {this.state.navigateToHome && <Navigate to="/home" />}
        <div className="modal-box">
          <div className="modal-topbar">
            <div className="logo-container">
              <VscWorkspaceTrusted className="logo" size={20} color="#66d37e" />
              <p className="logo-title-modal">Incendo</p>
            </div>
            <p className="page-title">&nbsp;|&nbsp;Portal signup</p>
          </div>
          <div className="modal-content">
            <div className="modal-input-container">
              <p className="modal-input-hint">Name</p>
              <input type="text" className="modal-input" onInput={(evt) => {
                this.setState({ name: evt.target.value, showError: false });
              }} />
            </div>
            <div className="modal-input-container">
              <p className="modal-input-hint">Email</p>
              <input type="text" className="modal-input" onInput={(evt) => {
                this.setState({ email: evt.target.value, showError: false });
              }} />
            </div>
            <div className="modal-input-container">
              <p className="modal-input-hint">Password</p>
              <input type="password" className="modal-input" onInput={(evt) => {
                this.setState({ password: evt.target.value, showError: false });
              }} />
            </div>
            {
              this.state.showError && <p className="error-box">{this.state.error}</p>
            }
            <button className="action-button" onClick={this.signup}>Continue</button>
            <p className="modal-notation">If you do have an account
              &nbsp;
              <Link to="/login">
                <span className="modal-action-text">login here.</span>
              </Link>
            </p>
          </div>
        </div>
      </div>
    )
  }
}

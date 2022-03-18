import React, { Component } from 'react'

import './Login.scss'

import { VscWorkspaceTrusted } from 'react-icons/vsc'
import { Link, Navigate } from 'react-router-dom'

export default class Login extends Component {

  state = {
    name: "",
    password: "",
    navigateToHome: false,
    showError: false,
    error: ""
  }

  login = () => {
    if (this.state.name.length == 0 || this.state.password.length == 0) {
      this.setState({ showError: true, error: "Please fill in all the fields" });
      return;
    }
    ApiRequests.post("users/register", {}, {
    }, false).then((response) => {
      Auth.setToken(response.body.token);
      this.setState({ navigateToHome: true });
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
            <p className="page-title">&nbsp;|&nbsp;Portal login</p>
          </div>
          <div className="modal-content">
            <div className="modal-input-container">
              <p className="modal-input-hint">Name</p>
              <input type="text" className="modal-input" onInput={(evt) => {
                this.setState({ name: evt.target.value });
              }} />
            </div>
            <div className="modal-input-container">
              <p className="modal-input-hint">Password</p>
              <input type="password" className="modal-input" onInput={(evt) => {
                this.setState({ password: evt.target.value });
              }} />
            </div>
            {
              this.state.showError && <p className="error-box">{this.state.error}</p>
            }
            <button className="action-button" onClick={this.login}>Continue</button>
            <p className="modal-notation">If you do not have an account
              &nbsp;
              <Link to="/signup">
                <span className="modal-action-text">create one.</span>
              </Link>
            </p>
          </div>
        </div>
      </div>
    )
  }
}

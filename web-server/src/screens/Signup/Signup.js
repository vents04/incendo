import React, { Component } from 'react'

import './Signup.scss'

import { VscWorkspaceTrusted } from 'react-icons/vsc'
import { Link, Navigate } from 'react-router-dom'
import ApiRequests from '../../classes/ApiRequests'
import Auth from '../../classes/Auth'

export default class Signup extends Component {

  state = {
    name: "",
    password: "",
    navigateToHome: false,
    showError: false,
    error: "",
    showLoading: false
  }

  signup = () => {
    this.setState({showError: false, error: "", showLoading: true});
    if (this.state.name.length == 0 || this.state.password.length == 0) {
      this.setState({ showError: true, error: "Please fill in all the fields" });
      return;
    }
    ApiRequests.post("users/register", {}, {
      name: this.state.name,
      password: this.state.password
    }, false).then((response) => {
      this.setState({ showLoading: false, navigateToHome: true });
      Auth.setToken(response.data.token);
    }).catch((error) => {
      this.setState({ showLoading: false });
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
      <div className="centered-content auth-page">
        {this.state.navigateToHome && <Navigate to="/home" />}
        <div className="modal-box">
          <div className="modal-topbar">
            <div className="logo-container">
              <VscWorkspaceTrusted className="logo" size={20} color="#66d37e" />
              <p className="logo-title-modal">Incendo</p>
            </div>
            <p className="page-title">Portal signup</p>
          </div>
          <div className="modal-content">
            <div className="modal-input-container">
              <p className="modal-input-hint">Name</p>
              <input type="text" className="modal-input" onInput={(evt) => {
                this.setState({ name: evt.target.value, showError: false });
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
            <button className="action-button" onClick={this.signup}>
              {this.state.showLoading ? "Signing up..." : "Continue"}
            </button>
            <p className="modal-notation">If you do have an account&nbsp;
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

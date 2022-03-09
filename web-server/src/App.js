import React from 'react';
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import EditCampaign from './screens/EditCampaign/EditCampaign';
import Home from './screens/Home/Home';
import Index from './screens/Index/Index';
import Login from './screens/Login/Login';
import Signup from './screens/Signup/Signup';
import ViewCampaign from './screens/ViewCampaign/ViewCampaign';

class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/home" element={<Home />} />
                    <Route path="/signup" element={<Signup />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/edit-campaign/:id" element={<EditCampaign />} />
                    <Route path="/view-campaign/:id" element={<ViewCampaign />} />
                </Routes>
            </BrowserRouter>
        )
    }
}

export default App;
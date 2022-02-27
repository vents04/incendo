import React from 'react';
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import Home from './screens/Home/Home';
import Index from './screens/Index/Index';
import Login from './screens/Login/Login';
import Signup from './screens/Signup/Signup';

class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Index />} />
                    <Route path="/home" element={<Home />} />
                    <Route path="/signup" element={<Signup />} />
                    <Route path="/login" element={<Login />} />
                </Routes>
            </BrowserRouter>
        )
    }
}

export default App;
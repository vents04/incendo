import React from 'react';
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import Contacts from './screens/Contacts/Contacts';
import Donate from './screens/Donate/Donate';
import Download from './screens/Download/Download';
import Home from './screens/Home/Home';
import Licensing from './screens/Licensing/Licensing';
import Organizations from './screens/Organizations/Organizations';
import PrivacyPolicy from './screens/PrivacyPolicy/PrivacyPolicy';
import WhitePaper from './screens/WhitePaper/WhitePaper';

class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/home" element={<Home />} />
                    <Route path="/white-paper" element={<WhitePaper />} />
                    <Route path="/organizations" element={<Organizations />} />
                    <Route path="/download" element={<Download />} />
                    <Route path="/contacts" element={<Contacts />} />
                    <Route path="/privacy-policy" element={<PrivacyPolicy />} />
                    <Route path="/licensing" element={<Licensing />} />
                    <Route path="/donate" element={<Donate />} />
                </Routes>
            </BrowserRouter>
        )
    }
}

export default App;
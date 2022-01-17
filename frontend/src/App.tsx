import { MainApp } from '@components/MainApp';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import { BaseLayout } from '@components/Elements/BaseLayout';

import './styles/App.css';
import 'antd/dist/antd.css';
import { Register } from '@components/Pages/Register';
import { CookiesProvider } from 'react-cookie';

const Home = () => (
    <>
        <h1>Home</h1>
        <p>This is the home page</p>
    </>
);

function App(): JSX.Element {
    return (
        <CookiesProvider>
            <BrowserRouter>
                <MainApp>
                    <BaseLayout>
                        <Routes>
                            <Route path="/" element={<Home />} />
                            <Route path="/register" element={<Register />} />
                        </Routes>
                    </BaseLayout>
                </MainApp>
            </BrowserRouter>
        </CookiesProvider>
    );
}

export default App;

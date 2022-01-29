import { MainApp } from '@components/MainApp';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import { BaseLayout } from '@components/Elements/BaseLayout';
import { Register } from '@components/Pages/Register';
import { CookiesProvider } from 'react-cookie';
import { Login } from '@components/Pages/Login';

import './styles/App.less';

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
                            <Route path="/login" element={<Login />} />
                        </Routes>
                    </BaseLayout>
                </MainApp>
            </BrowserRouter>
        </CookiesProvider>
    );
}

export default App;

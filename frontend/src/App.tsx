import { MainApp } from '@components/MainApp';
import { BaseLayout } from '@components/Elements/BaseLayout';
import './styles/App.css';
import 'antd/dist/antd.css';

function App(): JSX.Element {
    return (
        <MainApp>
            <BaseLayout>
                <h1>Digital Family Cookbook</h1>
            </BaseLayout>
        </MainApp>
    );
}

export default App;

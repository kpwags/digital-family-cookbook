import { useContext, useEffect } from 'react';
import {
    Typography,
} from 'antd';
import AppContext from '@contexts/AppContext';
import SiteSettingsForm from './SiteSettingsForm';

import './SiteSettings.less';

const { Title } = Typography;

const SiteSettings = (): JSX.Element => {
    const { siteSettings } = useContext(AppContext);

    useEffect(() => {
        document.title = `Site Settings - ${siteSettings.title}`;
    }, []);

    return (
        <div className="site-settings">
            <Title level={1}>Site Settings</Title>
            <SiteSettingsForm />
        </div>
    );
};

export default SiteSettings;

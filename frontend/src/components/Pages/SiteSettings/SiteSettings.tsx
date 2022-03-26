import { useContext, useEffect } from 'react';
import {
    Row,
    Col,
    Typography,
} from 'antd';
import AppContext from '@contexts/AppContext';
import SiteSettingsForm from './SiteSettingsForm';

const { Title } = Typography;

const SiteSettings = (): JSX.Element => {
    const { siteSettings } = useContext(AppContext);

    useEffect(() => {
        document.title = `Site Settings - ${siteSettings.title}`;
    }, []);

    return (
        <Row>
            <Col xs={12} offset={1}>
                <Title level={1}>Site Settings</Title>
                <SiteSettingsForm />
            </Col>
        </Row>
    );
};

export default SiteSettings;

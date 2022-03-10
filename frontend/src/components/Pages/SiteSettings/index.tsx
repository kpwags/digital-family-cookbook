import { useContext, useEffect } from 'react';
import {
    Row,
    Col,
    Typography,
} from 'antd';
import { SiteSettingsForm } from '@components/Forms/SiteSettingsForm';
import { AppContext } from '@contexts/AppContext';

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

export { SiteSettings };

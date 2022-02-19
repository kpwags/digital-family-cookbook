import {
    Row,
    Col,
    Typography,
} from 'antd';
import { SiteSettingsForm } from '@components/Forms/SiteSettingsForm';

const { Title } = Typography;

const SiteSettings = (): JSX.Element => (
    <Row>
        <Col xs={12} offset={1}>
            <Title level={1}>Site Settings</Title>
            <SiteSettingsForm />
        </Col>
    </Row>
);

export { SiteSettings };

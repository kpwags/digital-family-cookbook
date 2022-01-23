import { Row, Col, Typography } from 'antd';
import { LoginForm } from '@components/Forms/LoginForm';

const { Title, Paragraph } = Typography;

const Login = () => {
    const completeLoginProcess = () => {
        console.log('logging in user...');
    };

    return (
        <Row justify="center" align="top">
            <Col span={6}>
                <Title level={1}>Login</Title>
                <Paragraph>Enter your email and password to gain access.</Paragraph>
                <LoginForm onLoginCompleted={completeLoginProcess} />
            </Col>
        </Row>
    );
};

export { Login };

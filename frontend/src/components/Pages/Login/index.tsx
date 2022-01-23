import { Row, Col, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';
import { LoginForm } from '@components/Forms/LoginForm';
import { useContext } from 'react';
import { AppContext } from '@contexts/AppContext';

const { Title, Paragraph } = Typography;

const Login = () => {
    const navigate = useNavigate();

    const { loginUser } = useContext(AppContext);

    const completeLoginProcess = (token: string) => {
        loginUser(token);
        navigate('/');
    };

    return (
        <Row justify="center" align="top">
            <Col span={6}>
                <Title level={1}>Login</Title>
                <Paragraph>Enter your email and password to gain access.</Paragraph>
                <LoginForm onLoginCompleted={(token) => completeLoginProcess(token)} />
            </Col>
        </Row>
    );
};

export { Login };

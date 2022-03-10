import { Row, Col, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';
import { LoginForm } from '@components/Forms/LoginForm';
import { useContext, useEffect } from 'react';
import { AppContext } from '@contexts/AppContext';

const { Title, Paragraph } = Typography;

type LoginProps = {
    redirectTo?: string
}

const Login = ({
    redirectTo = '/',
}: LoginProps): JSX.Element => {
    const navigate = useNavigate();

    const { user, loginUser, siteSettings } = useContext(AppContext);

    const completeLoginProcess = (token: string) => {
        loginUser(token);
        navigate(redirectTo);
    };

    useEffect(() => {
        if (user !== null) {
            navigate('/');
        }
    }, [user]);

    useEffect(() => {
        document.title = `Login - ${siteSettings.title}`;
    }, []);

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

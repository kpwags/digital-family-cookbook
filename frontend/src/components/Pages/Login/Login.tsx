import { Row, Col, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';
import useQueryParameters from '@hooks/useQueryParameters';
import LoginForm from '@components/Forms/LoginForm';
import { useContext, useEffect } from 'react';
import AppContext from '@contexts/AppContext';
import AuthResult from '@models/AuthResult';

const { Title, Paragraph } = Typography;

type LoginProps = {
    redirectTo?: string
}

const Login = ({
    redirectTo = '/',
}: LoginProps): JSX.Element => {
    const navigate = useNavigate();

    const { user, loginUser, siteSettings } = useContext(AppContext);

    const query = useQueryParameters();
    const urlRedirect = query.get('redirect');

    const completeLoginProcess = (authResult: AuthResult) => {
        loginUser(authResult.accessToken);
        navigate(urlRedirect || redirectTo);
    };

    useEffect(() => {
        if (user !== null) {
            navigate(urlRedirect || redirectTo);
        }
    }, [user]);

    useEffect(() => {
        document.title = `Login - ${siteSettings.title}`;
    }, []);

    return (
        <Row justify="center" align="top">
            <Col span={8}>
                <Title level={1}>Login</Title>
                <Paragraph>Enter your email and password to gain access.</Paragraph>
                <LoginForm onLoginCompleted={completeLoginProcess} />
            </Col>
        </Row>
    );
};

export default Login;

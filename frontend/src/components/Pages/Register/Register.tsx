import { useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    Row,
    Col,
    Typography,
} from 'antd';
import { AppContext } from '@contexts/AppContext';
import RegisterForm from '@components/Forms/RegisterForm';

const { Title, Paragraph } = Typography;

const Register = (): JSX.Element => {
    const navigate = useNavigate();

    const { user, loginUser, siteSettings } = useContext(AppContext);

    useEffect(() => {
        if (user !== null) {
            navigate('/');
        }
    }, [user]);

    useEffect(() => {
        document.title = `Register - ${siteSettings.title}`;
    }, []);

    const completeRegistration = (token: string) => {
        loginUser(token);
        navigate('/');
    };

    return (
        <div className="register-page">
            <Row justify="center" align="top">
                <Col span={8}>
                    <Title level={1}>Register for an Account</Title>
                    <Paragraph>Complete the form below to create an account so you can start creating and organizing recipes</Paragraph>
                    <RegisterForm onRegisterCompleted={completeRegistration} />
                </Col>
            </Row>
        </div>
    );
};

export default Register;

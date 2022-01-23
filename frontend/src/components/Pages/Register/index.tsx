import { useState } from 'react';
import {
    Form,
    Input,
    Button,
    Row,
    Col,
    Typography,
    Spin,
    Alert,
} from 'antd';
import { Api } from '@lib/api';
import { useCookies } from 'react-cookie';
import { AuthResult } from '@models/AuthResult';

import './Register.css';

type FormValues = {
    name: string
    email: string
    password1: string
    password2: string
}

const { Title, Paragraph } = Typography;

const Register = (): JSX.Element => {
    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [, setCookie] = useCookies(['dfcuser']);

    const [form] = Form.useForm<FormValues>();

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Saving...');

        const [data, error] = await Api.Post<AuthResult>('auth/register', {
            data: {
                email: values.email,
                name: values.name,
                password: values.password1,
                confirmPassword: values.password2,
            },
        });

        if (error || data === null) {
            setErrorMessage(error || 'Error registerring user');
            setLoadingMessage('');
            return;
        }

        setCookie('dfcuser', data.token, { path: '/' });

        setLoadingMessage('');
    };

    const validatePassword = (field: unknown, value: string) => {
        let otherPassword = '';

        if ((field as { field: string }).field === 'password1') {
            otherPassword = form.getFieldValue('password2');
        } else {
            otherPassword = form.getFieldValue('password1');
        }

        if (typeof otherPassword === 'undefined' || otherPassword === '') {
            form.setFields([
                { name: 'password1', errors: [] },
                { name: 'password2', errors: [] },
            ]);

            return Promise.resolve();
        }

        if (otherPassword !== value) {
            form.setFields([
                { name: 'password1', errors: ['The two passwords that you entered do not match!'] },
                { name: 'password2', errors: ['The two passwords that you entered do not match!'] },
            ]);

            return Promise.reject(new Error('The two passwords that you entered do not match!'));
        }

        form.setFields([
            { name: 'password1', errors: [] },
            { name: 'password2', errors: [] },
        ]);

        return Promise.resolve();
    };

    return (
        <div className="register-page">
            <Spin
                size="large"
                spinning={loadingMessage !== ''}
                tip={loadingMessage}
            >
                <Row justify="center" align="top">
                    <Col span={12}>
                        <Title level={1}>Register for an Account</Title>
                        <Paragraph>Complete the form below to create an account so you can start creating and organizing recipes</Paragraph>

                        {errorMessage !== '' ? (
                            <Alert
                                type="error"
                                message={errorMessage}
                            />
                        ) : null}

                        <Form
                            className="register-form"
                            form={form}
                            labelAlign="right"
                            onFinish={(values: FormValues) => {
                                submitForm(values);
                            }}
                        >
                            <Form.Item
                                name="name"
                                label="Name"
                                rules={[
                                    { required: true, message: 'Name is required' },
                                ]}
                                required
                            >
                                <Input
                                    type="text"
                                />
                            </Form.Item>

                            <Form.Item
                                name="email"
                                label="Email"
                                rules={[
                                    { required: true, message: 'Email is required' },
                                    { type: 'email', message: 'Valid email is required' },
                                ]}
                                required
                            >
                                <Input
                                    type="email"
                                />
                            </Form.Item>

                            <Form.Item
                                name="password1"
                                label="Password"
                                rules={[
                                    { required: true, message: 'Password is required' },
                                    { validator: validatePassword },
                                ]}
                                required
                            >
                                <Input
                                    type="password"
                                />
                            </Form.Item>

                            <Form.Item
                                name="password2"
                                label="Re-Enter Password"
                                rules={[
                                    { required: true, message: 'Confirm Password is required' },
                                    { validator: validatePassword },
                                ]}
                                required
                            >
                                <Input
                                    type="password"
                                />
                            </Form.Item>

                            <Form.Item
                                className="action-area"
                            >
                                <Button
                                    type="primary"
                                    htmlType="submit"
                                >
                                    Register
                                </Button>
                            </Form.Item>

                        </Form>
                    </Col>
                </Row>
            </Spin>
        </div>
    );
};

export { Register };

import { useState } from 'react';
import {
    Form,
    Input,
    Button,
    Spin,
    Alert,
} from 'antd';
import { Api } from '@utils/api';
import AuthResult from '@models/AuthResult';

import './LoginForm.less';

type FormValues = {
    email: string
    password: string
}

type LoginFormProps = {
    onLoginCompleted: (authResult: AuthResult) => void
}

const LoginForm = ({
    onLoginCompleted,
}: LoginFormProps): JSX.Element => {
    const [form] = Form.useForm<FormValues>();

    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Logging In...');

        const [data, error] = await Api.Post<AuthResult>('auth/login', {
            data: {
                email: values.email,
                password: values.password,
            },
        });

        if (error || data === null) {
            setErrorMessage(error || 'Error logging in');
            setLoadingMessage('');
            return;
        }

        setLoadingMessage('');

        onLoginCompleted(data);
    };

    return (
        <div className="login-container">
            <Spin
                size="large"
                spinning={loadingMessage !== ''}
                tip={loadingMessage}
            >
                <>
                    {errorMessage !== '' ? (
                        <Alert
                            type="error"
                            message={errorMessage}
                        />
                    ) : null}

                    <Form
                        className="login-form"
                        form={form}
                        labelAlign="right"
                        onFinish={(values: FormValues) => {
                            submitForm(values);
                        }}
                    >
                        <Form.Item
                            name="email"
                            label="Email"
                            rules={[
                                { required: true, message: 'Email is required' },
                            ]}
                            required
                        >
                            <Input
                                type="email"
                            />
                        </Form.Item>

                        <Form.Item
                            name="password"
                            label="Password"
                            rules={[
                                { required: true, message: 'Password is required' },
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
                                Login
                            </Button>
                        </Form.Item>

                    </Form>
                </>
            </Spin>
        </div>
    );
};

export default LoginForm;

import { useState, useContext } from 'react';
import {
    Form,
    Input,
    Button,
    Spin,
    Alert,
} from 'antd';
import { Api } from '@utils/api';
import { AuthResult } from '@models/AuthResult';

import './RegisterForm.less';
import AppContext from '@contexts/AppContext';

type RegisterFormProps = {
    onRegisterCompleted: (token: string) => void
}

type FormValues = {
    name: string
    email: string
    password1: string
    password2: string
    invitationCode?: string
}

const RegisterForm = ({
    onRegisterCompleted,
}: RegisterFormProps): JSX.Element => {
    const [form] = Form.useForm<FormValues>();

    const { siteSettings } = useContext(AppContext);

    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Registerring...');

        const [data, error] = await Api.Post<AuthResult>('auth/register', {
            data: {
                email: values.email,
                name: values.name,
                password: values.password1,
                confirmPassword: values.password2,
                invitationCode: values.invitationCode || '',
            },
        });

        if (error || data === null) {
            setErrorMessage(error || 'Error registerring user');
            setLoadingMessage('');
            return;
        }

        setLoadingMessage('');
        onRegisterCompleted(data.token);
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
                { name: 'password1', errors: ['The passwords that you entered do not match'] },
                { name: 'password2', errors: ['The passwords that you entered do not match'] },
            ]);

            return Promise.reject(new Error('The passwords that you entered do not match'));
        }

        form.setFields([
            { name: 'password1', errors: [] },
            { name: 'password2', errors: [] },
        ]);

        return Promise.resolve();
    };

    return (
        <div className="register-container">

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

                        {!siteSettings.allowPublicRegistration ? (
                            <Form.Item
                                name="invitationCode"
                                label="Invitation Code"
                                rules={[
                                    { required: !siteSettings.allowPublicRegistration, message: 'Invitation Code is required' },
                                ]}
                                required
                            >
                                <Input
                                    type="text"
                                />
                            </Form.Item>
                        ) : null}

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
                </>
            </Spin>
        </div>
    );
};

export default RegisterForm;

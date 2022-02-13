import { useContext, useEffect, useState } from 'react';
import {
    Form,
    Input,
    Spin,
    Alert,
} from 'antd';
import { Api } from '@lib/api';
import { AppContext } from '@contexts/AppContext';
import { RoleType } from '@models/RoleType';
import { FormModal } from '@components/FormModal';

type FormValues = {
    name: string,
}

type RoleFormProps = {
    id?: string
    visible: boolean
    onSave: () => void
    onClose: () => void
}

const RoleForm = ({
    id = '',
    visible = false,
    onSave,
    onClose,
}: RoleFormProps): JSX.Element => {
    const [form] = Form.useForm<FormValues>();
    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');

    const { token } = useContext(AppContext);

    const getRoleType = async (id: string) => {
        setLoadingMessage('Loading...');
        const [data, error] = await Api.Get<RoleType>('system/getrolebyid', { params: { id }, token });

        if (error) {
            setErrorMessage(error);
            setLoadingMessage('');
            return;
        }

        form.setFieldsValue({
            name: data?.name,
        });
        setLoadingMessage('');
    };

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Saving...');
        const [, error] = await Api.Post('system/saverole', {
            token,
            data: {
                id,
                name: values.name,
            },
        });

        if (error) {
            setErrorMessage(error);
            setLoadingMessage('');
            return;
        }

        setLoadingMessage('');
        onSave();
    };

    useEffect(() => {
        if (visible) {
            if (id !== '') {
                getRoleType(id);
            } else {
                form.setFieldsValue({
                    name: '',
                });
            }
        }
    }, [visible, id]);

    return (
        <FormModal
            okText="Save"
            visible={visible}
            title={id === '' ? 'Add Role' : 'Edit Role'}
            onOk={() => {
                form.submit();
            }}
            onCancel={onClose}
        >
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
                        className="role-form"
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
                    </Form>
                </>
            </Spin>
        </FormModal>
    );
};

export { RoleForm };
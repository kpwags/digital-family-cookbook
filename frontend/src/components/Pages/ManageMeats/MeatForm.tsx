import { useEffect, useState } from 'react';
import {
    Form,
    Input,
    Spin,
    Alert,
} from 'antd';
import { Api } from '@utils/api';
import { Meat } from '@models/Meat';
import FormModal from '@components/FormModal';

type FormValues = {
    name: string,
}

type RoleFormProps = {
    id?: number
    visible: boolean
    currentMeats?: Meat[]
    onSave: () => void
    onClose: () => void
}

const MeatForm = ({
    id = 0,
    visible = false,
    currentMeats = [],
    onSave,
    onClose,
}: RoleFormProps): JSX.Element => {
    const [form] = Form.useForm<FormValues>();
    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');

    const resetForm = () => {
        form.resetFields();
        setErrorMessage('');
    };

    const getMeat = async (id: number) => {
        setLoadingMessage('Loading...');

        const [data, error] = await Api.Get<Meat>('meats/get', { params: { id } });

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

    const createMeat = async (categoryName: string): Promise<string | null> => {
        const [, error] = await Api.Post('meats/create', {
            data: {
                name: categoryName,
            },
        });

        return error;
    };

    const updateMeat = async (categoryName: string): Promise<string | null> => {
        const [, error] = await Api.Patch('meats/update', {
            data: {
                id,
                name: categoryName,
            },
        });

        return error;
    };

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Saving...');

        if (currentMeats.find((m) => m.name.toUpperCase() === values.name.toUpperCase().trim() && m.meatId !== id)) {
            setErrorMessage(`A meat with the name '${values.name}' already exists.`);
            setLoadingMessage('');
            return;
        }

        let error = null;

        if (id === 0) {
            error = await createMeat(values.name);
        } else {
            error = await updateMeat(values.name);
        }

        if (error) {
            setErrorMessage(error);
            setLoadingMessage('');
            return;
        }

        setLoadingMessage('');
        resetForm();
        onSave();
    };

    useEffect(() => {
        if (visible) {
            if (id !== 0) {
                getMeat(id);
            } else {
                form.setFieldsValue({
                    name: '',
                });
            }
        }
    }, [visible, id]);

    useEffect(() => {
        if (visible) {
            if (id !== 0) {
                getMeat(id);
            } else {
                form.setFieldsValue({
                    name: '',
                });
            }
        }
    }, []);

    return (
        <FormModal
            testId="meat-form-modal"
            okText="Save"
            visible={visible}
            title={id === 0 ? 'Add Meat' : 'Edit Meat'}
            onOk={() => {
                form.submit();
            }}
            onCancel={() => {
                resetForm();
                onClose();
            }}
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

export default MeatForm;

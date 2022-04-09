import { useEffect, useState } from 'react';
import {
    Form,
    Input,
    Spin,
    Alert,
} from 'antd';
import { Api } from '@utils/api';
import { Category } from '@models/Category';
import FormModal from '@components/FormModal';

type FormValues = {
    name: string,
}

type RoleFormProps = {
    id?: number
    visible: boolean
    currentCategories?: Category[]
    onSave: () => void
    onClose: () => void
}

const CategoryForm = ({
    id = 0,
    visible = false,
    currentCategories = [],
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

    const getCategory = async (id: number) => {
        setLoadingMessage('Loading...');

        const [data, error] = await Api.Get<Category>('categories/get', { params: { id } });

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

    const createCategory = async (categoryName: string): Promise<string | null> => {
        const [, error] = await Api.Post('categories/create', {
            data: {
                name: categoryName,
            },
        });

        return error;
    };

    const updateCategory = async (categoryName: string): Promise<string | null> => {
        const [, error] = await Api.Patch('categories/update', {
            data: {
                id,
                name: categoryName,
            },
        });

        return error;
    };

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Saving...');

        if (currentCategories.find((c) => c.name.toUpperCase() === values.name.toUpperCase().trim())) {
            setErrorMessage(`A cateogry with the name '${values.name}' already exists.`);
            setLoadingMessage('');
            return;
        }

        let error = null;

        if (id === 0) {
            error = await createCategory(values.name);
        } else {
            error = await updateCategory(values.name);
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
                getCategory(id);
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
            title={id === 0 ? 'Add Category' : 'Edit Category'}
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

export default CategoryForm;

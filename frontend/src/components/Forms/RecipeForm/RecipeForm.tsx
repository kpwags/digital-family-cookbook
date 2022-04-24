import { useState } from 'react';
import {
    Form,
    Button,
    Spin,
    Alert,
} from 'antd';
import { Api } from '@utils/api';
import Recipe from '@models/Recipe';
import TextInput from '@components/FormControls/TextInput';

type FormValues = {
    recipeId: number
    id: string
    name: string
    description?: string
    isPublic: boolean
    source?: string
    sourceUrl?: string
    time?: number
    activeTime?: number
    imageUrl?: string
    imageUrlLarge?: string
    calories?: number
    carbohydrates?: number
    sugar?: number
    fat?: number
    protein?: number
    fiber?: number
    cholesterol?: number
    meats: number[]
    categories: number[]
    ingredients: { name: string, sortOrder: number }[]
    steps: { name: string, sortOrder: number }[]
}

type RecipeFormProps = {
    onSave: (recipe: Recipe) => void
}

const RecipeForm = ({
    onSave,
}: RecipeFormProps): JSX.Element => {
    const [form] = Form.useForm<FormValues>();

    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Logging In...');

        const [data, error] = await Api.Post<Recipe>('recipes/create', {
            data: { values },
        });

        if (error || data === null) {
            setErrorMessage(error || 'Error logging in');
            setLoadingMessage('');
            return;
        }

        setLoadingMessage('');
        onSave(data);
    };

    return (
        <div className="recipe-form-container">
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
                        className="recipe-form"
                        form={form}
                        labelAlign="right"
                        onFinish={(values: FormValues) => {
                            submitForm(values);
                        }}
                    >
                        <TextInput
                            label="Name"
                            name="name"
                            required
                            rules={[
                                { required: true, message: 'Name is required' },
                            ]}
                        />

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

export default RecipeForm;

/* eslint-disable @typescript-eslint/no-unused-vars */
import { useState } from 'react';
import {
    Form,
    Button,
    Spin,
    Alert,
    Row,
    Col,
    Typography,
} from 'antd';
import { Api } from '@utils/api';
import Recipe from '@models/Recipe';
import TextInput from '@components/FormControls/TextInput';
import Switch from '@components/FormControls/Switch';

import './RecipeForm.less';
import RecipeIngredient from './RecipeIngredient';

const { Title } = Typography;

type IngredientStep = {
    name: string
    sortOrder: number
}

type FormValues = {
    recipeId: number
    id: string
    name: string
    description?: string
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
    ingredients: IngredientStep[]
    steps: IngredientStep[]
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
    const [isReciepPublic, setIsRecipePublic] = useState<boolean>(false);
    const [recipesIngredients, setRecipeIngredients] = useState<IngredientStep[]>([{
        sortOrder: 1,
        name: '',
    }]);

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Logging In...');

        const [data, error] = await Api.Post<Recipe>('recipes/create', {
            data: {
                isPublic: isReciepPublic,
                ...values,
            },
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
                        labelAlign="left"
                        onFinish={(values: FormValues) => {
                            submitForm(values);
                        }}
                    >
                        <Row>
                            <Col xs={24} md={18}>
                                <TextInput
                                    label="Name"
                                    name="name"
                                    required
                                    rules={[
                                        { required: true, message: 'Name is required' },
                                    ]}
                                />
                            </Col>
                            <Col xs={24} md={6}>
                                <Switch
                                    name="isPublic"
                                    label="Make Public"
                                    checked={isReciepPublic}
                                    onChange={(checked) => {
                                        setIsRecipePublic(checked);
                                    }}
                                />
                            </Col>
                        </Row>

                        <Row>
                            <Col xs={24} md={12}>
                                <TextInput
                                    label="Source"
                                    name="source"
                                />
                            </Col>
                            <Col xs={24} md={12}>
                                <TextInput
                                    label="Address"
                                    name="sourceUrl"
                                />
                            </Col>
                        </Row>

                        <Row>
                            <Col xs={24} md={12}>
                                <TextInput
                                    label="Time"
                                    name="time"
                                    mode="numeric"
                                />
                            </Col>
                            <Col xs={24} md={12}>
                                <TextInput
                                    label="Active Time"
                                    name="activeTime"
                                    mode="numeric"
                                />
                            </Col>
                        </Row>

                        <Row>
                            <Col xs={24} md={16}>
                                <Title level={3}>Ingredients</Title>
                                {recipesIngredients.map((ri) => (
                                    <RecipeIngredient
                                        key={ri.sortOrder}
                                        ingredient={ri.name}
                                        sortOrder={ri.sortOrder}
                                    />
                                ))}

                                <Title level={3}>Directions</Title>
                                <p>Directions</p>
                            </Col>
                            <Col xs={24} md={8}>
                                <TextInput
                                    label="Calories"
                                    name="calories"
                                    mode="numeric"
                                />
                                <TextInput
                                    label="Protein"
                                    name="protein"
                                    mode="numeric"
                                />
                                <TextInput
                                    label="Carbohydrates"
                                    name="carbohydrates"
                                    mode="numeric"
                                />
                                <TextInput
                                    label="Fat"
                                    name="fat"
                                    mode="numeric"
                                />
                                <TextInput
                                    label="Sugar"
                                    name="sugar"
                                    mode="numeric"
                                />
                                <TextInput
                                    label="Cholesterol"
                                    name="cholesterol"
                                    mode="numeric"
                                />
                                <TextInput
                                    label="Fiber"
                                    name="fiber"
                                    mode="numeric"
                                />
                            </Col>
                        </Row>

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

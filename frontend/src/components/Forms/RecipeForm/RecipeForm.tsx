import { useState, useContext } from 'react';
import {
    Form,
    Button,
    Spin,
    Alert,
    Row,
    Col,
    Typography,
    Space,
} from 'antd';
import { Api } from '@utils/api';
import AppContext from '@contexts/AppContext';
import Recipe from '@models/Recipe';
import TextInput from '@components/FormControls/TextInput';
import HtmlEditor from '@components/FormControls/HtmlEditor';
import Switch from '@components/FormControls/Switch';
import getMaxValue from '@utils/getMaxValue';
import Multiselect from '@components/FormControls/Multiselect/Multiselect';
import RecipeIngredient from './RecipeIngredient';
import RecipeDirection from './RecipeDirection';

import './RecipeForm.less';

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
}

type RecipeFormProps = {
    onSave: (recipe: Recipe) => void
}

const RecipeForm = ({
    onSave,
}: RecipeFormProps): JSX.Element => {
    const [form] = Form.useForm<FormValues>();

    const { categories, meats } = useContext(AppContext);

    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [isReciepPublic, setIsRecipePublic] = useState<boolean>(false);
    const [recipeIngredients, setRecipeIngredients] = useState<IngredientStep[]>([{
        name: '',
        sortOrder: 1,
    }]);
    const [recipeSteps, setRecipeSteps] = useState<IngredientStep[]>([{
        name: '',
        sortOrder: 1,
    }]);

    const submitForm = async (values: FormValues) => {
        setLoadingMessage('Logging In...');

        const [data, error] = await Api.Post<Recipe>('recipes/create', {
            data: {
                isPublic: isReciepPublic,
                ingredients: recipeIngredients,
                steps: recipeSteps,
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

    const addIngredient = () => {
        const maxSortOrder = getMaxValue(recipeIngredients.map((i) => i.sortOrder));

        setRecipeIngredients([
            ...recipeIngredients,
            { name: '', sortOrder: maxSortOrder + 1 },
        ]);
    };

    const updateIngredient = (idx: number, val: string) => {
        const newIngredients = [...recipeIngredients];

        newIngredients.filter((i) => i.sortOrder === idx)[0].name = val;

        setRecipeIngredients(newIngredients);
    };

    const removeIngredient = (sortOrder: number) => {
        const newIngredients = recipeIngredients.filter((i) => i.sortOrder !== sortOrder);

        if (newIngredients.length === 1) {
            setRecipeIngredients([{
                name: newIngredients[0].name,
                sortOrder: 1,
            }]);
        } else {
            setRecipeIngredients(newIngredients);
        }
    };

    const addDirection = () => {
        const maxSortOrder = getMaxValue(recipeSteps.map((i) => i.sortOrder));

        setRecipeSteps([
            ...recipeSteps,
            { name: '', sortOrder: maxSortOrder + 1 },
        ]);
    };

    const updateDirection = (idx: number, val: string) => {
        const newDirections = [...recipeSteps];

        newDirections.filter((d) => d.sortOrder === idx)[0].name = val;

        setRecipeSteps(newDirections);
    };

    const removeDirection = (sortOrder: number) => {
        const newDirections = recipeSteps.filter((i) => i.sortOrder !== sortOrder);

        if (newDirections.length === 1) {
            setRecipeSteps([{
                name: newDirections[0].name,
                sortOrder: 1,
            }]);
        } else {
            setRecipeSteps(newDirections);
        }
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
                        colon={false}
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

                        <HtmlEditor
                            name="description"
                            label="Description"
                        />

                        <Row>
                            <Col xs={24} md={12}>
                                <TextInput
                                    label="Servings"
                                    name="servings"
                                    mode="numeric"
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
                                    label="Web Address"
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

                        <Row className="ingredients-steps-nutrition">
                            <Col xs={24} md={16}>
                                <Title level={3}>Ingredients</Title>
                                <Space direction="vertical">
                                    <>
                                        {recipeIngredients.map((ri) => (
                                            <RecipeIngredient
                                                ingredientCount={recipeIngredients.length}
                                                key={ri.sortOrder}
                                                ingredient={ri.name}
                                                sortOrder={ri.sortOrder}
                                                onChange={(idx, val) => updateIngredient(idx, val)}
                                                onRemove={(idx) => removeIngredient(idx)}
                                            />
                                        ))}
                                    </>
                                    <div className="add-button-block">
                                        <Button
                                            type="ghost"
                                            onClick={addIngredient}
                                        >
                                            Add Ingredient
                                        </Button>
                                    </div>
                                </Space>

                                <Title level={3}>Directions</Title>
                                <Space direction="vertical">
                                    <>
                                        {recipeSteps.map((rs) => (
                                            <RecipeDirection
                                                directionCount={recipeSteps.length}
                                                key={rs.sortOrder}
                                                direction={rs.name}
                                                sortOrder={rs.sortOrder}
                                                onChange={(idx, val) => updateDirection(idx, val)}
                                                onRemove={(idx) => removeDirection(idx)}
                                            />
                                        ))}
                                    </>
                                    <div className="add-button-block">
                                        <Button
                                            type="ghost"
                                            onClick={addDirection}
                                        >
                                            Add Step
                                        </Button>
                                    </div>
                                </Space>
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

                        <Multiselect
                            name="categories"
                            label="Categories"
                            options={categories.map((c) => ({ value: c.categoryId, text: c.name }))}
                        />

                        <Multiselect
                            name="meats"
                            label="Meats"
                            options={meats.map((m) => ({ value: m.meatId, text: m.name }))}
                        />

                        <Form.Item
                            className="action-area"
                        >
                            <Button
                                type="primary"
                                htmlType="submit"
                            >
                                Save Recipe
                            </Button>
                        </Form.Item>

                    </Form>
                </>
            </Spin>
        </div>
    );
};

export default RecipeForm;

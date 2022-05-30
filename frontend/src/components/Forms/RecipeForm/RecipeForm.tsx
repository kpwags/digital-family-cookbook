import { useState, useContext, useEffect } from 'react';
import {
    Form,
    Button,
    Spin,
    Alert,
    Row,
    Col,
    Typography,
    Space,
    message,
} from 'antd';
import { DropResult } from 'react-beautiful-dnd';
import { Api } from '@utils/api';
import AppContext from '@contexts/AppContext';
import Recipe from '@models/Recipe';
import TextInput from '@components/FormControls/TextInput';
import NumericInput from '@components/FormControls/NumericInput';
import HtmlEditor from '@components/FormControls/HtmlEditor';
import Switch from '@components/FormControls/Switch';
import Uploader from '@components/FormControls/Uploader';
import getMaxValue from '@utils/getMaxValue';
import IngredientStep from '@models/IngredientStep';
import ImageUploadResponse from '@models/ImageUploadResponse';
import ConfirmDialog from '@components/ConfirmDialog';
import Multiselect from '@components/FormControls/Multiselect/Multiselect';
import { RcFile } from 'antd/lib/upload/interface';
import Ingredients from './Ingredients';
import Directions from './Directions';

import './RecipeForm.less';

const { Title, Paragraph } = Typography;

type FormValues = {
    id: string
    name: string
    servings: number
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
    recipe?: Recipe | null
    onSave: (recipe: Recipe) => void
}

const RecipeForm = ({
    recipe = null,
    onSave,
}: RecipeFormProps): JSX.Element => {
    const [form] = Form.useForm<FormValues>();

    const { categories, meats } = useContext(AppContext);

    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [isReciepPublic, setIsRecipePublic] = useState<boolean>(false);
    const [recipeIngredients, setRecipeIngredients] = useState<IngredientStep[]>([{
        id: 1,
        name: '',
        sortOrder: 1,
    }]);
    const [recipeSteps, setRecipeSteps] = useState<IngredientStep[]>([{
        id: 1,
        name: '',
        sortOrder: 1,
    }]);
    const [ingredientsError, setIngredientsError] = useState<string>('');
    const [directionsError, setDirectionsError] = useState<string>('');
    const [recipeImage, setRecipeImage] = useState<{ rootName: string, data: string }>({ rootName: '', data: '' });

    const resetForm = () => {
        form.resetFields();
        setIsRecipePublic(false);
        setRecipeIngredients([{
            id: 1,
            name: '',
            sortOrder: 1,
        }]);
        setRecipeSteps([{
            id: 1,
            name: '',
            sortOrder: 1,
        }]);
        setRecipeImage({ rootName: '', data: '' });
    };

    const getActualIngredientCount = (): number => recipeIngredients.filter((i) => i.name.trim() !== '').length;

    const getActualStepCount = (): number => recipeSteps.filter((s) => s.name.trim() !== '').length;

    const getValuesForRequest = (values: FormValues): Partial<FormValues> => {
        const requestValues: Partial<FormValues> = {
            name: values.name,
            description: values.description || '',
            servings: values.servings,
            source: values.source || '',
            sourceUrl: values.sourceUrl || '',
        };

        if (values.activeTime) {
            requestValues.activeTime = values.activeTime;
        }

        if (values.calories) {
            requestValues.calories = values.calories;
        }

        if (values.carbohydrates) {
            requestValues.carbohydrates = values.carbohydrates;
        }

        if (values.cholesterol) {
            requestValues.cholesterol = values.cholesterol;
        }

        if (values.fat) {
            requestValues.fat = values.fat;
        }

        if (values.fiber) {
            requestValues.fiber = values.fiber;
        }

        if (values.protein) {
            requestValues.protein = values.protein;
        }

        if (values.sugar) {
            requestValues.sugar = values.sugar;
        }

        if (values.time) {
            requestValues.time = values.time;
        }

        return requestValues;
    };

    const saveNewRecipe = async (values: Partial<FormValues>) => {
        const [data, error] = await Api.Post<Recipe>('recipes/create', {
            data: {
                isPublic: isReciepPublic,
                ingredients: recipeIngredients.filter((i) => i.name.trim() !== ''),
                steps: recipeSteps.filter((i) => i.name.trim() !== ''),
                imageFilename: recipeImage.rootName,
                ...values,
            },
        });

        if (error || data === null) {
            setErrorMessage(error || 'Error saving');
            setLoadingMessage('');
            return;
        }

        resetForm();
        setLoadingMessage('');
        onSave(data);
    };

    const updateRecipe = async (values: Partial<FormValues>) => {
        const [, error] = await Api.Patch('recipes/update', {
            data: {
                recipeId: recipe?.recipeId,
                isPublic: isReciepPublic,
                ingredients: recipeIngredients.filter((i) => i.name.trim() !== ''),
                steps: recipeSteps.filter((i) => i.name.trim() !== ''),
                imageFilename: recipeImage.rootName,
                ...values,
            },
        });

        if (error) {
            setErrorMessage(error);
            setLoadingMessage('');
            return;
        }

        message.success('Recipe Updated');
        setLoadingMessage('');
    };

    const submitForm = async (values: FormValues) => {
        window.scrollTo(0, 0);

        setErrorMessage('');
        setIngredientsError('');
        setDirectionsError('');

        const ingredientCount = getActualIngredientCount();
        const stepCount = getActualStepCount();

        if (ingredientCount <= 0 || stepCount <= 0) {
            if (ingredientCount <= 0) {
                setIngredientsError('Please enter an ingredient');
            }

            if (stepCount <= 0) {
                setDirectionsError('Please enter a step');
            }
            return;
        }

        setLoadingMessage('Saving...');

        const requestValues = getValuesForRequest(values);

        if (!recipe) {
            await saveNewRecipe(requestValues);
            return;
        }

        await updateRecipe(requestValues);
    };

    const addIngredient = () => {
        const maxSortOrder = getMaxValue(recipeIngredients.map((i) => i.sortOrder));
        const maxId = getMaxValue(recipeIngredients.map((i) => i.id));

        setRecipeIngredients([
            ...recipeIngredients,
            {
                id: maxId + 1,
                name: '',
                sortOrder: maxSortOrder + 1,
            },
        ]);
    };

    const updateIngredient = (idx: number, val: string) => {
        const newIngredients = [...recipeIngredients];

        newIngredients.filter((i) => i.sortOrder === idx)[0].name = val;

        setRecipeIngredients(newIngredients);
    };

    const removeIngredient = (id: number) => {
        const newIngredients = recipeIngredients.filter((i) => i.id !== id);

        if (newIngredients.length === 1) {
            setRecipeIngredients([{
                id: 1,
                name: newIngredients[0].name,
                sortOrder: 1,
            }]);
        } else {
            setRecipeIngredients(newIngredients);
        }
    };

    const addDirection = () => {
        const maxSortOrder = getMaxValue(recipeSteps.map((i) => i.sortOrder));
        const maxId = getMaxValue(recipeSteps.map((i) => i.id));

        setRecipeSteps([
            ...recipeSteps,
            {
                id: maxId + 1,
                name: '',
                sortOrder: maxSortOrder + 1,
            },
        ]);
    };

    const updateDirection = (id: number, val: string) => {
        const newDirections = [...recipeSteps];

        newDirections.filter((d) => d.id === id)[0].name = val;

        setRecipeSteps(newDirections);
    };

    const removeDirection = (id: number) => {
        const newDirections = recipeSteps.filter((i) => i.id !== id);

        if (newDirections.length === 1) {
            setRecipeSteps([{
                id: 1,
                name: newDirections[0].name,
                sortOrder: 1,
            }]);
        } else {
            setRecipeSteps(newDirections);
        }
    };

    const updateSort = (list: IngredientStep[]): IngredientStep[] => {
        const items: IngredientStep[] = [];
        let sortOrder = 1;

        list.forEach((i) => {
            items.push({
                id: i.id,
                name: i.name,
                sortOrder,
            });

            sortOrder += 1;
        });

        return items;
    };

    const reorder = (list: IngredientStep[], startIndex: number, endIndex: number) => {
        const result = Array.from(list);
        const [removed] = result.splice(startIndex, 1);
        result.splice(endIndex, 0, removed);

        return updateSort(result);
    };

    const onDragIngredientEnd = (result: DropResult) => {
        if (!result.destination) {
            return;
        }

        const items = reorder(
            recipeIngredients,
            result.source.index,
            result.destination.index,
        );

        setRecipeIngredients(items);
    };

    const onDragDirectionEnd = (result: DropResult) => {
        if (!result.destination) {
            return;
        }

        const items = reorder(
            recipeIngredients,
            result.source.index,
            result.destination.index,
        );

        setRecipeSteps(items);
    };

    const uploadImage = async (file?: RcFile): Promise<boolean> => {
        if (!file) {
            message.error('No file selected');
            return false;
        }

        const formData = new FormData();
        formData.append('image', file);

        const [imageData, uploadError] = await Api.PostWithUpload<ImageUploadResponse>('recipes/uploadimage', {
            data: formData,
        });

        if (uploadError) {
            message.error(uploadError);
            return false;
        }

        setRecipeImage({
            rootName: imageData?.filename || '',
            data: imageData?.imageData || '',
        });

        return true;
    };

    const deleteImage = async () => {
        setLoadingMessage('Delete Image...');

        const [data, error] = await Api.Post<Recipe>('recipes/deleteimage', {
            data: {
                imageFilename: recipeImage.rootName,
                recipeId: recipe ? recipe.recipeId : 0,
            },
        });

        if (error || data === null) {
            setErrorMessage(error || 'Error deleting image');
            setLoadingMessage('');
            return;
        }

        setLoadingMessage('');
        setRecipeImage({ rootName: '', data: '' });
    };

    useEffect(() => {
        if (recipe) {
            form.setFieldsValue({
                name: recipe.name,
                description: recipe.description,
                servings: recipe.servings,
                source: recipe.source,
                sourceUrl: recipe.sourceUrl,
                time: recipe.time,
                activeTime: recipe.activeTime,
                calories: recipe.calories,
                protein: recipe.protein,
                carbohydrates: recipe.carbohydrates,
                fat: recipe.fat,
                sugar: recipe.sugar,
                cholesterol: recipe.cholesterol,
                fiber: recipe.fiber,
                categories: recipe.categories.map((c) => c.categoryId),
                meats: recipe.meats.map((m) => m.meatId),
            });

            setIsRecipePublic(recipe.isPublic);

            if (recipe.imageUrl !== '') {
                setRecipeImage({
                    rootName: recipe.imageUrlLarge.replace('.jpg', ''),
                    data: recipe.imageData,
                });
            }

            if (recipe.ingredients.length > 0) {
                setRecipeIngredients(recipe.ingredients.map((i, idx) => ({ id: idx + 1, name: i.name, sortOrder: i.sortOrder || 0 })));
            }

            if (recipe.steps.length > 0) {
                setRecipeSteps(recipe.steps.map((s, idx) => ({ id: idx + 1, name: s.direction, sortOrder: s.sortOrder || 0 })));
            }
        }
    }, [recipe]);

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
                                <NumericInput
                                    label="Servings"
                                    name="servings"
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
                                    extra="If you got this from a website, put the URL here"
                                />
                            </Col>
                        </Row>

                        <Row>
                            <Col xs={24} md={12}>
                                <NumericInput
                                    label="Time"
                                    name="time"
                                    extra="Time to cook in minutes"
                                />
                            </Col>
                            <Col xs={24} md={12}>
                                <NumericInput
                                    label="Active Time"
                                    name="activeTime"
                                    extra="Active time spent cooking in minutes"
                                />
                            </Col>
                        </Row>

                        <Row className="ingredients-steps-nutrition">
                            <Col xs={24} md={16}>
                                <Title level={3}>Ingredients</Title>
                                {ingredientsError !== '' ? (
                                    <Paragraph type="danger" className="ingredients-error">{ingredientsError}</Paragraph>
                                ) : null}
                                <div className="ingredients-column">
                                    <Space direction="vertical">
                                        <Ingredients
                                            form={form}
                                            data={recipeIngredients}
                                            onDragEnd={onDragIngredientEnd}
                                            onIngredientUpdate={updateIngredient}
                                            onIngredientRemove={removeIngredient}
                                        />
                                        <div className="add-button-block">
                                            <Button
                                                type="ghost"
                                                onClick={addIngredient}
                                            >
                                                Add Ingredient
                                            </Button>
                                        </div>
                                    </Space>
                                </div>

                                <Title level={3}>Directions</Title>
                                {directionsError !== '' ? (
                                    <Paragraph type="danger" className="directions-error">{directionsError}</Paragraph>
                                ) : null}
                                <div className="ingredients-column">
                                    <Space direction="vertical">
                                        <Directions
                                            form={form}
                                            data={recipeSteps}
                                            onDragEnd={onDragDirectionEnd}
                                            onDirectionUpdate={updateDirection}
                                            onDirectionRemove={removeDirection}
                                        />
                                        <div className="add-button-block">
                                            <Button
                                                type="ghost"
                                                onClick={addDirection}
                                            >
                                                Add Step
                                            </Button>
                                        </div>
                                    </Space>
                                </div>
                            </Col>
                            <Col xs={24} md={8}>
                                <Title level={4}>Nutrition (per serving)</Title>
                                <NumericInput
                                    label="Calories"
                                    name="calories"
                                />
                                <NumericInput
                                    label="Protein"
                                    name="protein"
                                />
                                <NumericInput
                                    label="Carbohydrates"
                                    name="carbohydrates"
                                />
                                <NumericInput
                                    label="Fat"
                                    name="fat"
                                />
                                <NumericInput
                                    label="Sugar"
                                    name="sugar"
                                />
                                <NumericInput
                                    label="Cholesterol"
                                    name="cholesterol"
                                />
                                <NumericInput
                                    label="Fiber"
                                    name="fiber"
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

                        <Uploader
                            label="Image"
                            extra="PNG,JPG,GIF allowed. Must be < 4 MB"
                            name="recipeImage"
                            onUpload={uploadImage}
                            hidden={recipeImage.rootName !== ''}
                        />

                        <Space
                            direction="vertical"
                            size={8}
                            className="recipe-image"
                            hidden={recipeImage.rootName === ''}
                        >
                            <img src={recipeImage.data} alt="Recipe" />
                            {recipe === null ? (
                                <Button type="ghost" onClick={() => deleteImage()}>Delete</Button>
                            ) : (
                                <ConfirmDialog
                                    onConfirm={() => deleteImage()}
                                    text="Are you sure you want to delete this image? It cannot be undone."
                                >
                                    <Button type="ghost">Delete</Button>
                                </ConfirmDialog>
                            )}
                        </Space>

                        <Form.Item
                            className="action-area"
                        >
                            <Space direction="horizontal" size={12}>
                                <Button
                                    type="primary"
                                    htmlType="submit"
                                    disabled={loadingMessage !== ''}
                                >
                                    Save Recipe
                                </Button>
                                {!recipe ? (
                                    <Button
                                        type="ghost"
                                        htmlType="button"
                                        className="clear-form"
                                        disabled={loadingMessage !== ''}
                                        onClick={() => resetForm()}
                                    >
                                        Clear Form
                                    </Button>
                                ) : null}
                            </Space>
                        </Form.Item>

                    </Form>
                </>
            </Spin>
        </div>
    );
};

export default RecipeForm;

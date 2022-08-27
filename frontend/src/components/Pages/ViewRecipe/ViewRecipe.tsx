import Recipe from '@models/Recipe';
import { Api } from '@utils/api';
import { emptyQuillField, PageState } from '@utils/constants';
import {
    Alert,
    Col,
    Row,
    Spin,
    Space,
    Typography,
    Button,
    message,
} from 'antd';
import { HeartFilled, HeartOutlined } from '@ant-design/icons';
import useDocumentTitle from '@hooks/useDocumentTitle';
import { useParams } from 'react-router';
import { useEffect, useState, useContext } from 'react';
import AppContext from '@contexts/AppContext';
import HtmlViewer from '@components/HtmlViewer';
import BasicInfo from './BasicInfo';
import NutritionInfo from './NutritionInfo';
import RecipeImage from './RecipeImage';
import Categories from './Categories';
import Meats from './Meats';

import './ViewRecipe.less';

const { Title } = Typography;

const ViewRecipe = (): JSX.Element => {
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [recipe, setRecipe] = useState<Recipe | null>(null);

    const { user } = useContext(AppContext);

    const { id } = useParams();

    const fetchRecipe = async () => {
        if (!id) {
            setErrorMessage('Error loading recipe');
            setPageState(PageState.Error);
            return;
        }

        const [data, error] = await Api.Get<Recipe>(`recipes/get?id=${id}`);

        if (error) {
            setErrorMessage(error);
            setPageState(PageState.Error);
            return;
        }

        setRecipe(data);
        setPageState(PageState.Ready);
    };

    useEffect(() => {
        fetchRecipe();
    }, []);

    const toggleFavorite = async () => {
        let error: string | null = null;

        if (recipe?.isFavorite) {
            [, error] = await Api.Post('recipes/removefavorite', {
                data: {
                    userAccountId: user?.id,
                    recipeId: recipe?.recipeId,
                },
            });
        } else {
            [, error] = await Api.Post('recipes/markfavorite', {
                data: {
                    userAccountId: user?.id,
                    recipeId: recipe?.recipeId,
                },
            });
        }

        if (error) {
            message.error(error);
            return;
        }

        message.success(recipe?.isFavorite ? 'Removed as favorite' : 'Marked as favorite');

        fetchRecipe();
    };

    useDocumentTitle(recipe?.name || '');

    if (pageState === PageState.Loading) {
        return <Spin tip="Loading..." />;
    }

    if (pageState === PageState.Error) {
        return <Alert message={errorMessage} type="error" />;
    }

    if (recipe === null) {
        return <Alert message="Error loading recipe" type="error" />;
    }

    return (
        <article className="view-recipe">
            <Row justify="center">
                <Col lg={18} md={22}>
                    <Row align="middle" className="recipe-title">
                        <Col xs={22}>
                            <Title level={1}>{recipe.name}</Title>
                        </Col>
                        <Col xs={2} className="favorite-indicator">
                            <Button
                                type="link"
                                icon={recipe.isFavorite ? <HeartFilled /> : <HeartOutlined />}
                                onClick={() => toggleFavorite()}
                            />
                        </Col>
                    </Row>
                    <Row gutter={[24, 0]}>
                        <Col sm={16}>
                            <Space direction="vertical" size={24}>
                                {recipe.imageUrlLarge !== '' || recipe.largeImageData !== '' ? (
                                    <RecipeImage
                                        filename={recipe.imageUrlLarge}
                                        imageData={recipe.largeImageData}
                                        recipeName={recipe.name}
                                    />
                                ) : null}
                                {recipe.description !== emptyQuillField ? (
                                    <div className="description">
                                        <Title level={3}>Description</Title>
                                        <HtmlViewer html={recipe.description || ''} />
                                    </div>
                                ) : null}
                                <div className="ingredients">
                                    <Title level={3}>Ingredients</Title>
                                    <ul>
                                        {recipe.ingredients.map((i) => (
                                            <li key={i.id}>{i.name}</li>
                                        ))}
                                    </ul>
                                </div>
                                <div className="directions">
                                    <Title level={3}>Directions</Title>
                                    <ol>
                                        {recipe.steps.map((s) => (
                                            <li key={s.id}>{s.direction}</li>
                                        ))}
                                    </ol>
                                </div>
                            </Space>
                        </Col>
                        <Col sm={8} className="sidebar">
                            <Space direction="vertical" size={50}>
                                <BasicInfo recipe={recipe} />
                                <NutritionInfo recipe={recipe} />
                                <Categories categories={recipe.categories} />
                                <Meats meats={recipe.meats} />
                            </Space>
                        </Col>
                    </Row>
                </Col>
            </Row>
        </article>
    );
};

export default ViewRecipe;

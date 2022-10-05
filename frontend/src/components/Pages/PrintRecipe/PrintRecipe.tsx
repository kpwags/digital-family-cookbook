import {
    Spin, Alert, Typography, Row,
} from 'antd';
import useDocumentTitle from '@hooks/useDocumentTitle';
import { useParams } from 'react-router';
import { useState, useEffect } from 'react';
import Recipe from '@models/Recipe';
import { Api } from '@utils/api';
import { PageState } from '@utils/constants';
import convertTime from '@utils/convertTime';
import RecipeSource from '../ViewRecipe/RecipeSource';

import './PrintRecipe.less';

const { Title, Text } = Typography;

const PrintRecipe = (): JSX.Element => {
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [recipe, setRecipe] = useState<Recipe | null>(null);

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
        window.print();
    };

    useEffect(() => {
        fetchRecipe();
    }, []);

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
        <article className="print-recipe">
            <Title level={1}>{recipe.name}</Title>

            <Row justify="space-between" wrap className="recipe-details">
                <Text><Text strong>Servings:</Text> {recipe.servings}</Text>
                {(recipe.time || 0) > 0 ? <Text><Text strong>Time:</Text> {convertTime(recipe.time)}</Text> : null}
                {(recipe.activeTime || 0) > 0 ? <Text><Text strong>Active Time:</Text> {convertTime(recipe.activeTime)}</Text> : null}
                <Text><Text strong>Added By:</Text> {recipe.userAccount?.name}</Text>
                <RecipeSource recipe={recipe} />
            </Row>

            <div className="ingredients">
                <Title level={2}>Ingredients</Title>
                <ul>
                    {recipe.ingredients.map((i) => (
                        <li key={i.id}>{i.name}</li>
                    ))}
                </ul>
            </div>

            <div className="directions">
                <Title level={2}>Directions</Title>
                <ol>
                    {recipe.steps.map((s) => (
                        <li key={s.id}>{s.direction}</li>
                    ))}
                </ol>
            </div>

            <Row justify="space-between" wrap className="nutrition-details">
                {recipe.calories ? (
                    <Text><Text strong>Calories:</Text> {recipe.calories}</Text>
                ) : null}

                {recipe.protein ? (
                    <Text><Text strong>Protein:</Text> {recipe.protein}</Text>
                ) : null}

                {recipe.carbohydrates ? (
                    <Text><Text strong>Carbohydrates:</Text> {recipe.carbohydrates}</Text>
                ) : null}

                {recipe.fat ? (
                    <Text><Text strong>Fat:</Text> {recipe.fat}</Text>
                ) : null}

                {recipe.cholesterol ? (
                    <Text><Text strong>Cholesterol:</Text> {recipe.cholesterol}</Text>
                ) : null}

                {recipe.fiber ? (
                    <Text><Text strong>Fiber:</Text> {recipe.fiber}</Text>
                ) : null}

                {recipe.sugar ? (
                    <Text><Text strong>Sugar:</Text> {recipe.sugar}</Text>
                ) : null}
            </Row>
        </article>
    );
};

export default PrintRecipe;

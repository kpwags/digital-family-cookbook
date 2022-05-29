import RecipeForm from '@components/Forms/RecipeForm';
import {
    Typography,
    message,
} from 'antd';
import { useParams } from 'react-router';
import useDocumentTitle from '@hooks/useDocumentTitle';
import Recipe from '@models/Recipe';
import { useEffect, useState } from 'react';
import { Api } from '@utils/api';

const { Title } = Typography;

const EditRecipe = (): JSX.Element => {
    const [recipe, setRecipe] = useState<Recipe | null>(null);

    useDocumentTitle('Edit Recipe');

    const { id } = useParams();

    const loadRecipe = async () => {
        if (!id) {
            message.error('Error retrieving recipe');
            return;
        }

        const recipeId = parseInt(id, 10);

        const [data, error] = await Api.Get<Recipe>('recipes/get', {
            params: { id: recipeId },
        });

        if (error || data?.recipeId === 0) {
            message.error(error || 'Error retrieving recipe');
            return;
        }

        setRecipe(data);
    };

    useEffect(() => {
        loadRecipe();
    }, []);

    return (
        <>
            <Title level={1}>Edit Recipe</Title>

            <RecipeForm
                recipe={recipe}
                onSave={() => { message.success('Recipe created successfully'); }}
            />
        </>
    );
};

export default EditRecipe;

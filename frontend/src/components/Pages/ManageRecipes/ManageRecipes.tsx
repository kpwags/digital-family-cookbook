import {
    Button,
    message,
    Space,
    Typography,
} from 'antd';
import { useNavigate } from 'react-router';
import useDocumentTitle from '@hooks/useDocumentTitle';
import Recipe from '@models/Recipe';
import { Api } from '@utils/api';
import { useContext, useEffect, useState } from 'react';
import AppContext from '@contexts/AppContext';
import { hasRole } from '@utils/UserFunctions';
import RecipesGrid from './RecipesGrid';

const { Title } = Typography;

const ManageRecipes = (): JSX.Element => {
    const [loadingMessage, setLaodingMessage] = useState<string>('Loading...');
    const [recipes, setRecipes] = useState<Recipe[]>([]);

    const { user } = useContext(AppContext);

    const navigate = useNavigate();

    useDocumentTitle('Manage Recipes');

    const loadAllRecipes = async () => {
        const [data, error] = await Api.Get<Recipe[]>('recipes/getall');

        if (error) {
            message.error(error);
            setLaodingMessage('');
            return;
        }

        setRecipes(data || []);
        setLaodingMessage('');
    };

    const loadUserRecipes = async () => {
        const [data, error] = await Api.Get<Recipe[]>(
            'recipes/getuserrecipes',
            {
                params: {
                    userAccountId: user?.id || '',
                },
            },
        );

        if (error) {
            message.error(error);
            setLaodingMessage('');
            return;
        }

        setRecipes(data || []);
        setLaodingMessage('');
    };

    const loadRecipes = async () => {
        setLaodingMessage('Loading...');

        if (hasRole(user, 'ADMINISTRATOR')) {
            loadAllRecipes();
        } else {
            loadUserRecipes();
        }
    };

    const deleteRecipe = async (id: number) => {
        setLaodingMessage('Deleting Recipe...');

        const [, error] = await Api.Delete('recipes/delete', { params: { id } });

        if (error) {
            message.error(error);
            return;
        }

        loadRecipes();
    };

    useEffect(() => {
        loadRecipes();
    }, []);

    return (
        <>
            <Title level={1}>Manage Recipes</Title>

            <Space direction="vertical" size={24} className="full-width">
                <Button
                    type="primary"
                    onClick={() => navigate('/recipes/add')}
                >
                    Add Recipe
                </Button>

                <RecipesGrid
                    recipes={recipes}
                    loadingMessage={loadingMessage}
                    onEditRecipe={(id) => navigate(`/recipes/edit/${id}`)}
                    onDeleteRecipe={(id) => deleteRecipe(id)}
                />
            </Space>
        </>
    );
};

export default ManageRecipes;

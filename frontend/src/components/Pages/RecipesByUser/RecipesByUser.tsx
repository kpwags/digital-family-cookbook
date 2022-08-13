import { useEffect, useState } from 'react';
import { useParams } from 'react-router';
import {
    Typography,
    Spin,
    message,
} from 'antd';
import RecipeList from '@components/RecipeList';
import NoRecipes from '@components/NoRecipes';
import useDocumentTitle from '@hooks/useDocumentTitle';
import { Api } from '@utils/api';
import RecipeListPageResults from '@models/RecipeListPageResults';
import Recipe from '@models/Recipe';

const { Title } = Typography;

const RecipesByUser = (): JSX.Element => {
    const [processingMessage, setProcessingMessage] = useState<string>('Loading...');
    const [recipes, setRecipes] = useState<Recipe[]>([]);
    const [pageTitle, setPageTitle] = useState<string>('');

    useDocumentTitle('Recipes by Category');

    const { id } = useParams();

    const fetchRecipes = async () => {
        if (!id) {
            message.error('Error retrieving recipes');
            return;
        }

        const [data, error] = await Api.Get<RecipeListPageResults>('recipes/getrecipesbyuser', {
            params: { id, includeImages: true },
        });

        if (error || !data) {
            message.error(error || 'Error retrieving recipes');
            setProcessingMessage('');
            return;
        }

        setPageTitle(data.pageTitle);
        setRecipes(data.recipes);
        setProcessingMessage('');
    };

    useEffect(() => {
        fetchRecipes();
    }, [id]);

    return (
        <Spin
            spinning={processingMessage !== ''}
            tip={processingMessage}
        >
            <Title>Recipes Created By {pageTitle}</Title>

            {recipes.length > 0 || processingMessage !== '' ? (
                <RecipeList recipes={recipes} />
            ) : <NoRecipes pageText={`No recipes were found created by ${pageTitle}`} />}
        </Spin>
    );
};

export default RecipesByUser;

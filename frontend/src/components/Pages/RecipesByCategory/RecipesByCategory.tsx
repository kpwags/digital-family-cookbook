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

const RecipesByCategory = (): JSX.Element => {
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

        const categoryId = parseInt(id, 10);

        const [data, error] = await Api.Get<RecipeListPageResults>('recipes/getrecipesbycategory', {
            params: { id: categoryId, includeImages: true },
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
            <Title>Recipes Categorized &apos;{pageTitle}&apos;</Title>

            {recipes.length > 0 || processingMessage !== '' ? (
                <RecipeList recipes={recipes} />
            ) : <NoRecipes pageText={`No recipes were found with the category '${pageTitle}'`} />}
        </Spin>
    );
};

export default RecipesByCategory;
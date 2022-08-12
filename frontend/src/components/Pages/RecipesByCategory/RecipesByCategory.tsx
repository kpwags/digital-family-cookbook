import { useEffect, useState } from 'react';
import { useParams } from 'react-router';
import {
    Typography,
    Spin,
    message,
} from 'antd';
import RecipeList from '@components/RecipeList';
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
            message.error('Error retrieving recipe');
            return;
        }

        const categoryId = parseInt(id, 10);

        const [data, error] = await Api.Get<RecipeListPageResults>('recipes/getrecipesbycategory', {
            params: { id: categoryId },
        });

        if (error || !data) {
            message.error(error || 'Error retrieving recipe');
            setProcessingMessage('');
            return;
        }

        setPageTitle(data.pageTitle);
        setRecipes(data.recipes);
        setProcessingMessage('');
    };

    useEffect(() => {
        fetchRecipes();
    }, []);

    return (
        <Spin
            spinning={processingMessage !== ''}
            tip={processingMessage}
        >
            <Title>{pageTitle}</Title>

            <RecipeList recipes={recipes} />
        </Spin>
    );
};

export default RecipesByCategory;

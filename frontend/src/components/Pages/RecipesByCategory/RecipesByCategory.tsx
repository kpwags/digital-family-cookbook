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
import Pagination from '@components/Pagination';
import setPageUrl from '@utils/setPageUrl';

const { Title } = Typography;

const RecipesByCategory = (): JSX.Element => {
    const [processingMessage, setProcessingMessage] = useState<string>('Loading...');
    const [recipes, setRecipes] = useState<Recipe[]>([]);
    const [pageTitle, setPageTitle] = useState<string>('');
    const [currentPageNumber, setCurrentPageNumber] = useState<number>(1);
    const [recipesPerPage, setRecipesPerPage] = useState<number>(10);
    const [recipeCount, setRecipeCount] = useState<number>(0);

    useDocumentTitle('Recipes by Category');

    const { id, page } = useParams();

    const fetchRecipes = async (pageNum = 1) => {
        if (!id) {
            message.error('Error retrieving recipes');
            return;
        }

        const categoryId = parseInt(id, 10);

        const [data, error] = await Api.Get<RecipeListPageResults>('recipes/getrecipesbycategory', {
            params: {
                categoryId,
                includeImages: true,
                pageNumber: pageNum,
                recipesPerPage,
            },
        });

        if (error || !data) {
            message.error(error || 'Error retrieving recipes');
            setProcessingMessage('');
            return;
        }

        setPageTitle(data.pageTitle);
        setRecipes(data.recipes);
        setRecipeCount(data.totalRecipeCount);
        setProcessingMessage('');
    };

    useEffect(() => {
        const pageNumber = page ? parseInt(page, 10) : 1;
        setCurrentPageNumber(pageNumber);
        fetchRecipes(pageNumber);
    }, [id, page]);

    return (
        <Spin
            spinning={processingMessage !== ''}
            tip={processingMessage}
        >
            <Title>Recipes Categorized &apos;{pageTitle}&apos;</Title>

            {recipes.length > 0 || processingMessage !== '' ? (
                <>
                    <RecipeList recipes={recipes} />
                    <Pagination
                        recipeCount={recipeCount}
                        currentPageNumber={currentPageNumber}
                        recipesPerPage={recipesPerPage}
                        onChange={(pageNumber, pageSize) => {
                            setRecipesPerPage(pageSize);
                            setCurrentPageNumber(pageNumber);
                            fetchRecipes(pageNumber);

                            setPageUrl(`/recipes/category/${id}/${pageNumber}`);
                        }}
                    />
                </>
            ) : <NoRecipes pageText={`No recipes were found with the category '${pageTitle}'`} />}
        </Spin>
    );
};

export default RecipesByCategory;

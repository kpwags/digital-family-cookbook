import { useEffect, useState } from 'react';
import { useParams } from 'react-router';
import {
    Typography,
    Spin,
    message,
    Row,
    Col,
    Segmented,
} from 'antd';
import {
    AppstoreOutlined,
    MenuOutlined,
    UnorderedListOutlined,
} from '@ant-design/icons';
import RecipeList from '@components/RecipeList';
import BasicRecipeList from '@components/BasicRecipeList';
import RecipeGrid from '@components/RecipeGrid';
import NoRecipes from '@components/NoRecipes';
import Pagination from '@components/Pagination';
import useDocumentTitle from '@hooks/useDocumentTitle';
import { Api } from '@utils/api';
import setPageUrl from '@utils/setPageUrl';
import RecipeListPageResults from '@models/RecipeListPageResults';
import Recipe from '@models/Recipe';
import LocalStorageUtils from '@utils/LocalStorageUtils';

const { Title } = Typography;

enum ViewMode {
    Grid = 'grid',
    List = 'list',
    BasicList = 'basiclist',
}

type RecipeListingProps = {
    mode: 'category' | 'meat' | 'user' | 'all'
}

const RecipeListing = ({
    mode = 'all',
}: RecipeListingProps): JSX.Element => {
    const [processingMessage, setProcessingMessage] = useState<string>('Loading...');
    const [recipes, setRecipes] = useState<Recipe[]>([]);
    const [pageTitle, setPageTitle] = useState<string>('');
    const [noRecipesMessage, setNoRecipesMessage] = useState<string>('No Recipes Found');
    const [currentPageNumber, setCurrentPageNumber] = useState<number>(1);
    const [recipesPerPage, setRecipesPerPage] = useState<number>(10);
    const [recipeCount, setRecipeCount] = useState<number>(0);
    const [currentViewMode, setCurrentViewMode] = useState<ViewMode>(ViewMode.Grid);

    const { id, page } = useParams();

    // eslint-disable-next-line max-len
    const fetchRecipesByCategory = async (id: number, pageNum = 1): Promise<[RecipeListPageResults | null, string | null]> => Api.Get<RecipeListPageResults>('recipes/getrecipesbycategory', {
        params: {
            categoryId: id,
            includeImages: true,
            pageNumber: pageNum,
            recipesPerPage,
        },
    });

    // eslint-disable-next-line max-len
    const fetchRecipesByMeat = async (id: number, pageNum = 1): Promise<[RecipeListPageResults | null, string | null]> => Api.Get<RecipeListPageResults>('recipes/getrecipesbymeat', {
        params: {
            meatId: id,
            includeImages: true,
            pageNumber: pageNum,
            recipesPerPage,
        },
    });

    // eslint-disable-next-line max-len
    const fetchRecipesByUser = async (id: string, pageNum = 1): Promise<[RecipeListPageResults | null, string | null]> => Api.Get<RecipeListPageResults>('recipes/getrecipesbyuser', {
        params: {
            userAccountId: id,
            includeImages: true,
            pageNumber: pageNum,
            recipesPerPage,
        },
    });

    // eslint-disable-next-line max-len
    const fetchAllRecipes = async (pageNum = 1): Promise<[RecipeListPageResults | null, string | null]> => Api.Get<RecipeListPageResults>('recipes/getallrecipes', {
        params: {
            includeImages: true,
            pageNumber: pageNum,
            recipesPerPage,
        },
    });

    const fetchRecipes = async (pageNum = 1) => {
        if (mode !== 'all' && !id) {
            message.error('Error retrieving recipes');
            return;
        }

        let data: RecipeListPageResults | null = null;
        let error: string | null = null;

        switch (mode) {
            case 'category':
                [data, error] = await fetchRecipesByCategory(parseInt(id || '0', 10), pageNum);
                break;

            case 'meat':
                [data, error] = await fetchRecipesByMeat(parseInt(id || '0', 10), pageNum);
                break;

            case 'user':
                [data, error] = await fetchRecipesByUser(id || '0', pageNum);
                break;

            case 'all':
                [data, error] = await fetchAllRecipes(pageNum);
                break;

            default:
                break;
        }

        if (error || !data) {
            message.error(error || 'Error retrieving recipes');
            setProcessingMessage('');
            return;
        }

        switch (mode) {
            case 'category':
                setPageTitle(`Recipes Categorized '${data.pageTitle}'`);
                setNoRecipesMessage(`No recipes were found with the category '${data.pageTitle}'`);
                break;

            case 'meat':
                setPageTitle(`Recipes with ${data.pageTitle}`);
                setNoRecipesMessage(`No recipes were found with the meat '${data.pageTitle}'`);
                break;

            case 'user':
                setPageTitle(`Recipes Created By ${data.pageTitle}`);
                setNoRecipesMessage(`No recipes were found created by ${data.pageTitle}`);
                break;

            case 'all':
            default:
                setPageTitle('All Recipes');
                setNoRecipesMessage('No recipes were found');
                break;
        }

        setRecipes(data.recipes);
        setRecipeCount(data.totalRecipeCount);
        setProcessingMessage('');
    };

    const persistView = (view: ViewMode) => {
        LocalStorageUtils.setValue('default_recipe_view', view.toString());
    };

    const getDocumentTitle = (mode: 'category' | 'meat' | 'user' | 'all'): string => {
        switch (mode) {
            case 'category':
                return 'Recipes by Category';

            case 'meat':
                return 'Recipes by Meat';

            case 'user':
                return 'Recipes by User';

            case 'all':
            default:
                return 'Recipes';
        }
    };

    useDocumentTitle(getDocumentTitle(mode));

    useEffect(() => {
        const pageNumber = page ? parseInt(page, 10) : 1;
        setCurrentPageNumber(pageNumber);
        fetchRecipes(pageNumber);

        const localStorageView = LocalStorageUtils.getValue('default_recipe_view');
        setCurrentViewMode(localStorageView as ViewMode || ViewMode.Grid);
    }, [id, page, mode]);

    return (
        <Spin
            spinning={processingMessage !== ''}
            tip={processingMessage}
        >
            <Row>
                <Col xs={24} sm={24} md={16}>
                    <Title>{pageTitle}</Title>
                </Col>
                <Col xs={24} sm={24} className="recipe-view-toggle">
                    <Segmented
                        value={currentViewMode}
                        options={[
                            {
                                label: 'Grid',
                                value: 'grid',
                                icon: <AppstoreOutlined />,
                            },
                            {
                                label: 'List',
                                value: 'list',
                                icon: <UnorderedListOutlined />,
                            },
                            {
                                label: 'Basic List',
                                value: 'basiclist',
                                icon: <MenuOutlined />,
                            },
                        ]}
                        onChange={(value) => {
                            switch (value) {
                                case 'list':
                                    persistView(ViewMode.List);
                                    setCurrentViewMode(ViewMode.List);
                                    break;
                                case 'basiclist':
                                    persistView(ViewMode.BasicList);
                                    setCurrentViewMode(ViewMode.BasicList);
                                    break;
                                case 'grid':
                                default:
                                    persistView(ViewMode.Grid);
                                    setCurrentViewMode(ViewMode.Grid);
                            }
                        }}
                    />
                </Col>
            </Row>

            {recipes.length > 0 || processingMessage !== '' ? (
                <>
                    <div hidden={currentViewMode !== ViewMode.Grid}>
                        <RecipeGrid recipes={recipes} />
                        <Pagination
                            recipeCount={recipeCount}
                            currentPageNumber={currentPageNumber}
                            recipesPerPage={recipesPerPage}
                            onChange={(pageNumber, pageSize) => {
                                setRecipesPerPage(pageSize);
                                setCurrentPageNumber(pageNumber);
                                fetchRecipes(pageNumber);

                                setPageUrl(`/recipes/${mode}/${id}/${pageNumber}`);
                            }}
                        />
                    </div>
                    <div hidden={currentViewMode !== ViewMode.List}>
                        <RecipeList
                            recipeCount={recipeCount}
                            recipes={recipes}
                            currentPageNumber={currentPageNumber}
                            recipesPerPage={recipesPerPage}
                            onChange={(pageNumber, pageSize) => {
                                setRecipesPerPage(pageSize);
                                setCurrentPageNumber(pageNumber);
                                fetchRecipes(pageNumber);

                                setPageUrl(`/recipes/${mode}/${id}/${pageNumber}`);
                            }}
                        />
                    </div>
                    <div hidden={currentViewMode !== ViewMode.BasicList}>
                        <BasicRecipeList recipes={recipes} />
                        <Pagination
                            recipeCount={recipeCount}
                            currentPageNumber={currentPageNumber}
                            recipesPerPage={recipesPerPage}
                            onChange={(pageNumber, pageSize) => {
                                setRecipesPerPage(pageSize);
                                setCurrentPageNumber(pageNumber);
                                fetchRecipes(pageNumber);

                                setPageUrl(`/recipes/${mode}/${id}/${pageNumber}`);
                            }}
                        />
                    </div>
                </>
            ) : <NoRecipes pageText={noRecipesMessage} />}
        </Spin>
    );
};

export default RecipeListing;

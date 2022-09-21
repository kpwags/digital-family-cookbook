import { useState, useEffect } from 'react';
import {
    Col,
    Row,
    Typography,
    Spin,
    Segmented,
    message,
} from 'antd';
import {
    AppstoreOutlined,
    MenuOutlined,
    UnorderedListOutlined,
} from '@ant-design/icons';
import SearchForm from '@components/Forms/SearchForm';
import { Api } from '@utils/api';
import RecipeListPageResults from '@models/RecipeListPageResults';
import ViewMode from '@models/ViewMode';
import LocalStorageUtils from '@utils/LocalStorageUtils';
import Recipe from '@models/Recipe';
import Pagination from '@components/Pagination';
import RecipeGrid from '@components/RecipeGrid';
import setPageUrl from '@utils/setPageUrl';
import BasicRecipeList from '@components/BasicRecipeList';
import RecipeList from '@components/RecipeList';
import NoRecipes from '@components/NoRecipes';
import useQueryParameters from '@hooks/useQueryParameters';
import useDocumentTitle from '@hooks/useDocumentTitle';

const { Title } = Typography;

const Search = (): JSX.Element => {
    const [processingMessage, setProcessingMessage] = useState<string>('');
    const [searchKeywords, setSearchKeywords] = useState<string>('');
    const [hasSearched, setHasSearched] = useState<boolean>(false);
    const [currentPageNumber, setCurrentPageNumber] = useState<number>(1);
    const [recipesPerPage, setRecipesPerPage] = useState<number>(10);
    const [currentViewMode, setCurrentViewMode] = useState<ViewMode>(ViewMode.Grid);
    const [recipes, setRecipes] = useState<Recipe[]>([]);
    const [recipeCount, setRecipeCount] = useState<number>(0);

    const query = useQueryParameters();

    const searchRecipes = async (keywords: string, page = 1, pageCount = 10) => {
        setProcessingMessage('Loading...');

        setSearchKeywords(keywords);

        const [data, error] = await Api.Get<RecipeListPageResults>('recipes/search', {
            params: {
                keywords,
                includeImages: true,
                pageNumber: page,
                recipesPerPage: pageCount,
            },
        });

        if (error || !data) {
            message.error(error || 'Unable to search recipes');
            setProcessingMessage('');
            return;
        }

        setHasSearched(true);
        setRecipes(data.recipes);
        setRecipeCount(data.totalRecipeCount);
        setPageUrl(`/search?q=${keywords}&p=${page}&rpp=${pageCount}`);
        setProcessingMessage('');
    };

    const persistView = (view: ViewMode) => {
        LocalStorageUtils.setValue('default_recipe_view', view.toString());
    };

    useDocumentTitle(`Search Results for '${searchKeywords}'`);

    useEffect(() => {
        const q = query.get('q');
        const p = query.get('p');
        const rpp = query.get('rpp');

        const pageNumber = p ? parseInt(p, 10) : 1;
        setCurrentPageNumber(pageNumber);

        const perPage = rpp ? parseInt(rpp, 10) : 10;
        setRecipesPerPage(perPage);

        if ((q || '') !== '') {
            searchRecipes(q || '');
        }

        const localStorageView = LocalStorageUtils.getValue('default_recipe_view');
        setCurrentViewMode(localStorageView as ViewMode || ViewMode.Grid);
    }, [query]);

    return (
        <>
            <Title level={1}>Search</Title>

            <SearchForm
                keywords={searchKeywords}
                processingMessage={processingMessage}
                onSearch={(keywords) => searchRecipes(keywords)}
            />

            {hasSearched ? (
                <Spin
                    spinning={processingMessage !== ''}
                    tip={processingMessage}
                >
                    <Row>
                        <Col xs={24} sm={24} md={16}>
                            <Title level={2}>Search Results for &apos;{searchKeywords}&apos;</Title>
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
                                        searchRecipes(searchKeywords, pageNumber, pageSize);

                                        setPageUrl(`/search?q=${searchKeywords}&p=${pageNumber}&rpp=${pageSize}`);
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
                                        searchRecipes(searchKeywords, pageNumber, pageSize);

                                        setPageUrl(`/search?q=${searchKeywords}&p=${pageNumber}&rpp=${pageSize}`);
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
                                        searchRecipes(searchKeywords, pageNumber, pageSize);

                                        setPageUrl(`/search?q=${searchKeywords}&p=${pageNumber}&rpp=${pageSize}`);
                                    }}
                                />
                            </div>
                        </>
                    ) : (
                        <NoRecipes
                            pageText={`No recipes found for '${searchKeywords}'`}
                            showBackButton={false}
                        />
                    )}
                </Spin>
            ) : null}
        </>
    );
};

export default Search;

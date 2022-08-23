import Recipe from './Recipe';

interface RecipeListPageResults {
    pageTitle: string
    recipes: Recipe[]
    pageCount: number
    totalRecipeCount: number
}

export default RecipeListPageResults;

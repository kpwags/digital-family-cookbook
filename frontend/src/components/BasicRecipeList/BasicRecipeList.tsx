import Recipe from '@models/Recipe';
import { Link } from 'react-router-dom';

import './BasicRecipeList.less';

type RecipeListProps = {
    recipes: Recipe[]
}

const RecipeList = ({
    recipes,
}: RecipeListProps): JSX.Element => (
    <ul className="basic-recipe-list">
        {recipes.map((r) => (
            <li key={r.recipeId}><Link to={`/recipes/view/${r.recipeId}`}>{r.name}</Link></li>
        ))}
    </ul>
);

export default RecipeList;

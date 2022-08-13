import { useNavigate } from 'react-router';
import { Card } from 'antd';
import Recipe from '@models/Recipe';

import './RecipeCard.less';

const { Meta } = Card;

type RecipeCardProps = {
    recipe: Recipe
}

const RecipeCard = ({
    recipe,
}: RecipeCardProps): JSX.Element => {
    const navigate = useNavigate();

    return (
        <Card
            className="recipe-card"
            data-testid={`recipe-card-${recipe.recipeId}`}
            cover={recipe.imageUrl !== '' ? (
                <img
                    alt={recipe.name}
                    src={recipe.imageData}
                />
            ) : (
                <img
                    alt={recipe.name}
                    src="/images/recipe.jpg"
                />
            )}
            onClick={() => navigate(`/recipes/view/${recipe.recipeId}`)}
        >
            <Meta
                title={recipe.name}
            />
        </Card>
    );
};

export default RecipeCard;

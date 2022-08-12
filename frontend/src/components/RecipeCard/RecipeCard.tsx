import {
    Card,
} from 'antd';
import Recipe from '@models/Recipe';
import HtmlViewer from '@components/HtmlViewer';

type RecipeCardProps = {
    recipe: Recipe
}

const RecipeCard = ({
    recipe,
}: RecipeCardProps): JSX.Element => (
    <Card title={recipe.name}>
        <HtmlViewer html={recipe.description || '<p>No Description</p>'} />
    </Card>
);

export default RecipeCard;

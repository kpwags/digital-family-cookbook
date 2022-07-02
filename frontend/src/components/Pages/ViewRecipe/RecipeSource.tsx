import { Typography } from 'antd';
import Recipe from '@models/Recipe';

const { Text } = Typography;

const RecipeSource = ({ recipe }: { recipe: Recipe }): JSX.Element | null => {
    if (recipe?.source === '') {
        return null;
    }

    if (recipe?.sourceUrl !== '') {
        return (
            <Text><Text strong>Source:</Text> <a href={recipe.sourceUrl} target="_blank" rel="noreferrer nofollow">{recipe.source}</a></Text>
        );
    }

    return (
        <Text><Text strong>Source:</Text> <>{recipe.source}</></Text>
    );
};

export default RecipeSource;

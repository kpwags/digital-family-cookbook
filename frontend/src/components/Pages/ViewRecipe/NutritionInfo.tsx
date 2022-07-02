import Recipe from '@models/Recipe';
import { Space, Typography } from 'antd';

const { Title, Text } = Typography;

type NutritionInfoProps = {
    recipe: Recipe
}

const NutritionInfo = ({
    recipe,
}: NutritionInfoProps): JSX.Element | null => {
    const hasNutrtionInformation = (): boolean => {
        if (recipe.calories
            || recipe.protein
            || recipe.carbohydrates
            || recipe.sugar
            || recipe.fiber
            || recipe.fat
            || recipe.cholesterol) {
            return true;
        }

        return false;
    };

    if (!hasNutrtionInformation()) {
        return null;
    }

    return (
        <Space direction="vertical" className="nutrition">
            <Title level={3}>Nutrition Info</Title>
            {recipe.calories ? <Text><Text strong>Calories:</Text> {recipe.calories}</Text> : null}

            {recipe.protein ? <Text><Text strong>Protein:</Text> {recipe.protein}g</Text> : null}

            {recipe.carbohydrates ? <Text><Text strong>Carbohydrates:</Text> {recipe.carbohydrates}g</Text> : null}

            {recipe.fat ? <Text><Text strong>Fat:</Text> {recipe.fat}g</Text> : null}

            {recipe.cholesterol ? <Text><Text strong>Cholesterol:</Text> {recipe.cholesterol}mg</Text> : null}

            {recipe.fiber ? <Text><Text strong>Fiber:</Text> {recipe.fiber}g</Text> : null}

            {recipe.sugar ? <Text><Text strong>Sugar:</Text> {recipe.sugar}g</Text> : null}
        </Space>
    );
};

export default NutritionInfo;

import Recipe from '@models/Recipe';
import { Space, Typography, Row } from 'antd';

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

            {recipe.calories ? (
                <Row justify="space-between">
                    <Text strong>Calories:</Text>
                    <Text>{recipe.calories}</Text>
                </Row>
            ) : null}

            {recipe.protein ? (
                <Row justify="space-between" className="highlighted">
                    <Text strong>Protein:</Text>
                    <Text>{recipe.protein}g</Text>
                </Row>
            ) : null}

            {recipe.carbohydrates ? (
                <Row justify="space-between">
                    <Text strong>Carbohydrates:</Text>
                    <Text>{recipe.carbohydrates}g</Text>
                </Row>
            ) : null}

            {recipe.fat ? (
                <Row justify="space-between" className="highlighted">
                    <Text strong>Fat:</Text>
                    <Text>{recipe.fat}g</Text>
                </Row>
            ) : null}

            {recipe.cholesterol ? (
                <Row justify="space-between">
                    <Text strong>Cholesterol:</Text>
                    <Text>{recipe.cholesterol}mg</Text>
                </Row>
            ) : null}

            {recipe.fiber ? (
                <Row justify="space-between" className="highlighted">
                    <Text strong>Fiber:</Text>
                    <Text>{recipe.fiber}g</Text>
                </Row>
            ) : null}

            {recipe.sugar ? (
                <Row justify="space-between">
                    <Text strong>Sugar:</Text>
                    <Text>{recipe.sugar}g</Text>
                </Row>
            ) : null}
        </Space>
    );
};

export default NutritionInfo;

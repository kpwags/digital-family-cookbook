import { useEffect, useState } from 'react';
import { Spin, Row, Col } from 'antd';
import RecipeCard from '@components/RecipeCard';
import { Api } from '@utils/api';
import Recipe from '@models/Recipe';

type MostFavoritedRecipesProps = {
    count?: number;
}

const MostFavoritedRecipes = ({
    count = 8,
}: MostFavoritedRecipesProps): JSX.Element => {
    const [recipes, setRecipes] = useState<Recipe[]>([]);
    const [processingMessage, setProcessingMessage] = useState<string>('Loading...');

    const fetchRecipes = async () => {
        const [data, error] = await Api.Get<Recipe[]>('recipes/getmostfavoritedrecipes', { params: { count, includeImages: true } });

        if (error) {
            setProcessingMessage('');
            return;
        }

        setRecipes(data || []);
        setProcessingMessage('');
    };

    useEffect(() => {
        fetchRecipes();
    }, []);

    return (
        <div style={{ marginTop: '2rem' }}>
            <Spin
                size="large"
                tip={processingMessage}
                spinning={processingMessage !== ''}
            >
                <h2>Most Favorited Recipes</h2>
                <Row gutter={[16, 16]}>
                    {recipes.map((r) => (
                        <Col xs={24} sm={24} md={12} lg={6} key={r.id}>
                            <RecipeCard recipe={r} />
                        </Col>
                    ))}
                </Row>
            </Spin>
        </div>
    );
};

export default MostFavoritedRecipes;

import {
    Row,
    Col,
} from 'antd';
import RecipeCard from '@components/RecipeCard';
import Recipe from '@models/Recipe';

type RecipeListProps = {
    recipes: Recipe[]
}

const RecipeList = ({
    recipes,
}: RecipeListProps): JSX.Element => (
    <div>
        <Row gutter={[16, 16]}>
            {recipes.map((r) => (
                <Col xs={24} sm={24} md={12} lg={6} key={r.id}>
                    <RecipeCard recipe={r} />
                </Col>
            ))}
        </Row>
    </div>
);

export default RecipeList;

import {
    List,
    Tag,
    Space,
    Typography,
} from 'antd';
import Recipe from '@models/Recipe';
import { Link } from 'react-router-dom';
import getTagColor from '@utils/getTagColor';

import './RecipeList.less';

const { Title, Text } = Typography;

type RecipeListProps = {
    recipes: Recipe[]
    recipeCount: number
    currentPageNumber: number
    recipesPerPage: number
    onChange: (pageNumber: number, pageSize: number) => void
}

const RecipeList = ({
    recipes,
    recipeCount,
    currentPageNumber,
    recipesPerPage,
    onChange,
}: RecipeListProps): JSX.Element => (
    <div className="recipe-list">
        <List
            itemLayout="vertical"
            size="large"
            pagination={{
                onChange,
                total: recipeCount,
                showTotal: (total, range) => `${range[0]}-${range[1]} of ${total} recipes`,
                defaultCurrent: 1,
                current: currentPageNumber,
                defaultPageSize: 10,
                pageSize: recipesPerPage,
                showSizeChanger: true,
                style: { textAlign: 'center', margin: '2rem 0' },
            }}
            dataSource={recipes}
            renderItem={(recipe) => (
                <List.Item
                    key={recipe.recipeId}
                    extra={recipe.imageData !== '' ? (
                        <img
                            width={272}
                            alt={recipe.name}
                            src={recipe.imageData}
                        />
                    ) : (
                        <img
                            width={272}
                            alt={recipe.name}
                            src="/images/recipe.jpg"
                        />
                    )}
                >
                    <Title level={3}><Link to={`/recipes/view/${recipe.recipeId}`}>{recipe.name}</Link></Title>
                    <Space direction="vertical" size={8}>
                        <Text><Text strong>Added By:</Text> {recipe.userAccount?.name}</Text>
                        {recipe.categories.length > 0 ? (
                            <Space direction="horizontal" wrap>
                                <Text strong>Categories:</Text>
                                {recipe.categories.map((c, idx) => (
                                    <Tag color={getTagColor(idx)} key={c.categoryId}>{c.name}</Tag>
                                ))}
                            </Space>
                        ) : null}
                        {recipe.meats.length > 0 ? (
                            <Space direction="horizontal" wrap>
                                <Text strong>Meats:</Text>
                                {recipe.meats.map((m, idx) => (
                                    <Tag color={getTagColor(idx)} key={m.meatId}>{m.name}</Tag>
                                ))}
                            </Space>
                        ) : null}
                    </Space>
                </List.Item>
            )}
        />
    </div>
);

export default RecipeList;

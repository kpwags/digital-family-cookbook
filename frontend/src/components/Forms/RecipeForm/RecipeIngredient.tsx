import { DeleteOutlined } from '@ant-design/icons';
import { Space, Input, Button } from 'antd';

type RecipeIngredientProps = {
    ingredient: string
    sortOrder: number
}

const RecipeIngredient = ({
    ingredient,
    sortOrder,
}: RecipeIngredientProps): JSX.Element => (
    <Space direction="horizontal" size={2}>
        <Input
            id={`ingredient-${sortOrder}`}
            value={ingredient}
        />
        <Button
            type="link"
            hidden={sortOrder <= 1}
        >
            <DeleteOutlined style={{ color: '#ff000' }} />
        </Button>
    </Space>
);

export default RecipeIngredient;

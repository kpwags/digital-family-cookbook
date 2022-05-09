import { DeleteOutlined } from '@ant-design/icons';
import {
    Space, Input, Button, Form,
} from 'antd';

type RecipeIngredientProps = {
    ingredientCount: number
    ingredient: string
    sortOrder: number
    onChange: (idx: number, val: string) => void
    onRemove: (idx: number) => void
}

const RecipeIngredient = ({
    ingredientCount,
    ingredient,
    sortOrder,
    onChange,
    onRemove,
}: RecipeIngredientProps): JSX.Element => (
    <Space direction="horizontal" size={2} className="ingredients">
        <Form.Item
            label=" "
            name={`ingredient-${sortOrder}`}
        >
            <Input
                id={`ingredient-${sortOrder}`}
                value={ingredient}
                onChange={(e) => {
                    e.preventDefault();
                    onChange(sortOrder, e.target.value);
                }}
            />
        </Form.Item>
        <Button
            type="link"
            hidden={ingredientCount <= 1}
            onClick={() => onRemove(sortOrder)}
        >
            <DeleteOutlined style={{ fill: '#ff000' }} />
        </Button>
    </Space>
);

export default RecipeIngredient;

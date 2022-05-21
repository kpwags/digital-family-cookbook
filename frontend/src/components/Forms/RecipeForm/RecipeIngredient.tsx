import { DeleteOutlined } from '@ant-design/icons';
import {
    Space,
    Input,
    Button,
    Form,
} from 'antd';
import IngredientStep from '@models/IngredientStep';

type RecipeIngredientProps = {
    ingredientCount: number
    ingredient: IngredientStep
    onChange: (id: number, val: string) => void
    onRemove: (id: number) => void
}

const RecipeIngredient = ({
    ingredientCount,
    ingredient,
    onChange,
    onRemove,
}: RecipeIngredientProps): JSX.Element => (
    <Space direction="horizontal" size={2} className="ingredients">
        <Form.Item
            label=" "
            name={`ingredient-${ingredient.id}`}
        >
            <Input
                id={`ingredient-${ingredient.id}`}
                data-testid={`ingredient-${ingredient.id}`}
                value={ingredient.name}
                onChange={(e) => {
                    e.preventDefault();
                    onChange(ingredient.id, e.target.value);
                }}
            />
        </Form.Item>
        <Button
            type="link"
            className="delete-ingredient-step"
            data-testid={`delete-ingredient-${ingredient.id}`}
            hidden={ingredientCount <= 1}
            onClick={() => onRemove(ingredient.id)}
        >
            <DeleteOutlined style={{ fill: '#ff000' }} />
        </Button>
    </Space>
);

export default RecipeIngredient;

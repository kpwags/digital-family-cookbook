import { DeleteOutlined } from '@ant-design/icons';
import {
    Space, Input, Button, Form,
} from 'antd';
import IngredientStep from '@models/IngredientStep';

const { TextArea } = Input;

type RecipeIngredientProps = {
    directionCount: number
    direction: IngredientStep
    onChange: (id: number, val: string) => void
    onRemove: (id: number) => void
}

const RecipeIngredient = ({
    directionCount,
    direction,
    onChange,
    onRemove,
}: RecipeIngredientProps): JSX.Element => (
    <Space direction="horizontal" size={2} className="directions">
        <Form.Item
            label=" "
            name={`step-${direction.id}`}
        >
            <TextArea
                id={`direction-${direction.id}`}
                value={direction.name}
                data-testid={`direction-${direction.id}`}
                onChange={(e) => {
                    e.preventDefault();
                    onChange(direction.id, e.target.value);
                }}
            />
        </Form.Item>
        <Button
            type="link"
            className="delete-ingredient-step"
            hidden={directionCount <= 1}
            data-testid={`delete-step-${direction.id}`}
            onClick={() => onRemove(direction.id)}
        >
            <DeleteOutlined style={{ fill: '#ff000' }} />
        </Button>
    </Space>
);

export default RecipeIngredient;

import { DeleteOutlined } from '@ant-design/icons';
import {
    Space, Input, Button, Form,
} from 'antd';

const { TextArea } = Input;

type RecipeIngredientProps = {
    directionCount: number
    direction: string
    sortOrder: number
    onChange: (idx: number, val: string) => void
    onRemove: (idx: number) => void
}

const RecipeIngredient = ({
    directionCount,
    direction,
    sortOrder,
    onChange,
    onRemove,
}: RecipeIngredientProps): JSX.Element => (
    <Space direction="horizontal" size={2} className="directions">
        <Form.Item
            label=" "
            name={`step-${sortOrder}`}
        >
            <TextArea
                id={`direction-${sortOrder}`}
                value={direction}
                onChange={(e) => {
                    e.preventDefault();
                    onChange(sortOrder, e.target.value);
                }}
            />
        </Form.Item>
        <Button
            type="link"
            hidden={directionCount <= 1}
            onClick={() => onRemove(sortOrder)}
        >
            <DeleteOutlined style={{ fill: '#ff000' }} />
        </Button>
    </Space>
);

export default RecipeIngredient;

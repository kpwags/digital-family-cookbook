import { DeleteOutlined } from '@ant-design/icons';
import {
    Space,
    Input,
    Button,
    Form,
    FormInstance,
} from 'antd';
import IngredientStep from '@models/IngredientStep';
import { useState, useEffect } from 'react';

const { TextArea } = Input;

type RecipeIngredientProps = {
    form: FormInstance
    directionCount: number
    data: IngredientStep
    onChange: (id: number, val: string) => void
    onRemove: (id: number) => void
}

const RecipeIngredient = ({
    form,
    directionCount,
    data,
    onChange,
    onRemove,
}: RecipeIngredientProps): JSX.Element => {
    const [direction, setDirection] = useState<IngredientStep>(data);

    useEffect(() => {
        setDirection(data);
        form.setFieldsValue({
            [`step-${data.id}`]: data.name,
        });
    }, [data]);

    return (
        <Space direction="horizontal" size={2} className="directions">
            <Form.Item
                label=" "
                name={`step-${direction.id}`}
            >
                <TextArea
                    id={`direction-${direction.id}`}
                    data-testid={`direction-input-${direction.id}`}
                    onChange={(e) => {
                        e.preventDefault();
                        onChange(direction.id, e.target.value);
                    }}
                />
            </Form.Item>
            <Button
                type="link"
                className="delete-step"
                hidden={directionCount <= 1}
                data-testid={`delete-step-${direction.id}`}
                onClick={() => onRemove(direction.id)}
            >
                <DeleteOutlined style={{ fill: '#ff000' }} />
            </Button>
        </Space>
    );
};

export default RecipeIngredient;

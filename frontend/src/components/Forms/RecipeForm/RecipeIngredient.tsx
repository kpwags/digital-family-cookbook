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

type RecipeIngredientProps = {
    form: FormInstance
    ingredientCount: number
    data: IngredientStep
    onChange: (id: number, val: string) => void
    onRemove: (id: number) => void
}

const RecipeIngredient = ({
    form,
    ingredientCount,
    data,
    onChange,
    onRemove,
}: RecipeIngredientProps): JSX.Element => {
    const [ingredient, setIngredient] = useState<IngredientStep>(data);

    useEffect(() => {
        setIngredient(data);
        form.setFieldsValue({
            [`ingredient-${data.id}`]: data.name,
        });
    }, [data]);

    return (
        <Space direction="horizontal" size={2} className="ingredients">
            <Form.Item
                label=" "
                name={`ingredient-${ingredient.id}`}
            >
                <Input
                    id={`ingredient-${ingredient.id}`}
                    data-testid={`ingredient-${ingredient.id}`}
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
};

export default RecipeIngredient;

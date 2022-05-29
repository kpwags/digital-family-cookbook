import {
    DragDropContext,
    Droppable,
    Draggable,
    DropResult,
} from 'react-beautiful-dnd';
import { HolderOutlined } from '@ant-design/icons';
import IngredientStep from '@models/IngredientStep';
import { useEffect, useState } from 'react';
import { FormInstance } from 'antd';
import RecipeIngredient from './RecipeIngredient';

type IngredientsProps = {
    form: FormInstance
    data: IngredientStep[]
    onDragEnd: (result: DropResult) => void
    onIngredientUpdate: (idx: number, val: string) => void
    onIngredientRemove: (id: number) => void
}

const Ingredients = ({
    form,
    data,
    onDragEnd,
    onIngredientUpdate,
    onIngredientRemove,
}: IngredientsProps): JSX.Element => {
    const [ingredients, setIngredients] = useState<IngredientStep[]>(data);

    useEffect(() => {
        setIngredients(data);
    }, [data]);

    return (
        <DragDropContext onDragEnd={onDragEnd}>
            <Droppable droppableId="ingredients">
                {(provided) => (
                    <div
                        ref={provided.innerRef}
                        {...provided.droppableProps}
                    >
                        {ingredients.map((i, index) => (
                            <Draggable
                                key={i.id}
                                draggableId={`s_${i.id}`}
                                index={index}
                            >
                                {(provided) => (
                                    <div
                                        className="recipe-ingredient"
                                        ref={provided.innerRef}
                                        {...provided.draggableProps}
                                        {...provided.dragHandleProps}
                                    >
                                        <HolderOutlined />
                                        <RecipeIngredient
                                            form={form}
                                            ingredientCount={ingredients.length}
                                            key={i.sortOrder}
                                            data={i}
                                            onChange={(id, val) => onIngredientUpdate(id, val)}
                                            onRemove={(id) => onIngredientRemove(id)}
                                        />
                                    </div>
                                )}
                            </Draggable>
                        ))}
                        {provided.placeholder}
                    </div>
                )}
            </Droppable>
        </DragDropContext>
    );
};

export default Ingredients;

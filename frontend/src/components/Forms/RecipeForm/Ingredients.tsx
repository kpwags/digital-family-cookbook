import {
    DragDropContext,
    Droppable,
    Draggable,
    DropResult,
} from 'react-beautiful-dnd';
import { HolderOutlined } from '@ant-design/icons';
import IngredientStep from '@models/IngredientStep';
import RecipeIngredient from './RecipeIngredient';

type IngredientsProps = {
    ingredients: IngredientStep[]
    onDragEnd: (result: DropResult) => void
    onIngredientUpdate: (idx: number, val: string) => void
    onIngredientRemove: (id: number) => void
}

const Ingredients = ({
    ingredients,
    onDragEnd,
    onIngredientUpdate,
    onIngredientRemove,
}: IngredientsProps): JSX.Element => (
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
                                        ingredientCount={ingredients.length}
                                        key={i.sortOrder}
                                        ingredient={i}
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

export default Ingredients;

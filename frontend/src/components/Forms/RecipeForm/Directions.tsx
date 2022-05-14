import {
    DragDropContext,
    Droppable,
    Draggable,
    DropResult,
} from 'react-beautiful-dnd';
import { HolderOutlined } from '@ant-design/icons';
import IngredientStep from '@models/IngredientStep';
import RecipeDirection from './RecipeDirection';

type DirectionsProps = {
    directions: IngredientStep[]
    onDragEnd: (result: DropResult) => void
    onDirectionUpdate: (idx: number, val: string) => void
    onDirectionRemove: (id: number) => void
}

const Directions = ({
    directions,
    onDragEnd,
    onDirectionUpdate,
    onDirectionRemove,
}: DirectionsProps): JSX.Element => (
    <DragDropContext onDragEnd={onDragEnd}>
        <Droppable droppableId="directions">
            {(provided) => (
                <div
                    ref={provided.innerRef}
                    {...provided.droppableProps}
                >
                    {directions.map((d, index) => (
                        <Draggable
                            key={d.id}
                            draggableId={`d_${d.id}`}
                            index={index}
                        >
                            {(provided) => (
                                <div
                                    className="recipe-step"
                                    ref={provided.innerRef}
                                    {...provided.draggableProps}
                                    {...provided.dragHandleProps}
                                >
                                    <HolderOutlined className="dragger" />
                                    <RecipeDirection
                                        directionCount={directions.length}
                                        key={d.sortOrder}
                                        direction={d}
                                        onChange={(id, val) => onDirectionUpdate(id, val)}
                                        onRemove={(id) => onDirectionRemove(id)}
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

export default Directions;

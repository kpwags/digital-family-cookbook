import {
    Table,
    Button,
} from 'antd';
import { ColumnsType } from 'antd/lib/table';
import ConfirmDialog from '@components/ConfirmDialog';
import Recipe from '@models/Recipe';

type RecipesGridProps = {
    recipes: Recipe[]
    loadingMessage: string
    onEditRecipe: (id: number) => void
    onDeleteRecipe: (id: number) => void
}

const RecipesGrid = ({
    recipes = [],
    loadingMessage = '',
    onEditRecipe,
    onDeleteRecipe,
}: RecipesGridProps): JSX.Element => {
    const columns: ColumnsType<Recipe> = [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'name',
            width: '55%',
        },
        {
            key: 'addedBy',
            title: 'Added By',
            width: '25%',
            render: (_, recipe) => (
                <>{recipe?.userAccount?.name}</>
            ),
        },
        {
            key: 'actions',
            title: 'Actions',
            render: (_, recipe) => (
                <>
                    <Button type="link" onClick={() => onEditRecipe(recipe.recipeId)}>Edit</Button>
                    <ConfirmDialog
                        onConfirm={() => { onDeleteRecipe(recipe.recipeId); }}
                        text={`Are you sure you want to delete ${recipe.name}?`}
                    >
                        <Button type="link">Delete</Button>
                    </ConfirmDialog>
                </>
            ),
        },
    ];

    return (
        <>
            <Table
                rowKey={(record) => record.id}
                columns={columns}
                dataSource={recipes}
                pagination={false}
                loading={{
                    spinning: loadingMessage !== '',
                    tip: loadingMessage,
                }}
            />
        </>
    );
};

export default RecipesGrid;

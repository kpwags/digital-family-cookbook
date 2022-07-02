import {
    Table,
    Button,
} from 'antd';
import { ColumnsType } from 'antd/lib/table';
import ConfirmDialog from '@components/ConfirmDialog';
import Recipe from '@models/Recipe';
import Sorter from '@utils/Sorter';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

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
    const [uniqueUsers, setUniqueUsers] = useState<{ text: string, value: string }[]>([]);

    useEffect(() => {
        const userIds = recipes.map((r) => r.userAccountId || '');
        const uniqueUserIds: string[] = [...new Set(userIds)];

        const users: { text: string, value: string }[] = [];

        uniqueUserIds.forEach((id) => {
            users.push({
                text: recipes.filter((r) => r.userAccountId === id)[0].userAccount?.name || '',
                value: id,
            });
        });

        setUniqueUsers(users.filter((u) => u.value !== '' && u.text !== ''));
    }, [recipes]);

    const columns: ColumnsType<Recipe> = [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'name',
            width: '55%',
            sortDirections: ['ascend', 'descend'],
            defaultSortOrder: 'ascend',
            sorter: (a, b) => Sorter.SortStrings(a.name, b.name),
            render: (_, recipe) => (
                <Link to={`/recipes/view/${recipe.recipeId}`}>{recipe.name}</Link>
            ),
        },
        {
            key: 'addedBy',
            title: 'Added By',
            width: '25%',
            filters: uniqueUsers,
            filterMode: 'tree',
            sortDirections: ['ascend', 'descend'],
            defaultSortOrder: 'ascend',
            sorter: (a, b) => Sorter.SortStrings(a?.userAccount?.name || '', b?.userAccount?.name || ''),
            onFilter: (value, record) => {
                if (record.userAccount) {
                    return record.userAccount.id === value.toString();
                }

                return false;
            },
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

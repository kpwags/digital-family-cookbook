import { useState } from 'react';
import {
    Table,
    Button,
    Alert,
} from 'antd';
import { ColumnsType } from 'antd/lib/table';
import ConfirmDialog from '@components/ConfirmDialog';
import { Api } from '@utils/api';
import { Category } from '@models/Category';

type CategoriesGridProps = {
    categories: Category[]
    loadingMessage: string
    onEditCategory: (id: number) => void
    onCategoryChanged: () => void
}

const CategoriesGrid = ({
    categories = [],
    loadingMessage = '',
    onEditCategory,
    onCategoryChanged,
}: CategoriesGridProps): JSX.Element => {
    const [errorMessage, setErrorMessage] = useState<string>('');

    const deleteRole = async (id: string) => {
        const [, error] = await Api.Post('categories/delete', { data: { id }, method: 'DELETE' });

        if (error) {
            setErrorMessage(error);
            return;
        }

        onCategoryChanged();
    };

    const columns: ColumnsType<Category> = [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'name',
            width: '80%',
        },
        {
            key: 'actions',
            title: 'Actions',
            render: (_, category: Category) => (
                <>
                    <Button type="link" onClick={() => onEditCategory(category.categoryId)}>Edit</Button>
                    <ConfirmDialog
                        onConfirm={() => { deleteRole(category.id); }}
                        text={`Are you sure you want to delete ${category.name}?`}
                    >
                        <Button type="link">Delete</Button>
                    </ConfirmDialog>
                </>
            ),
        },
    ];

    return (
        <>
            {errorMessage !== '' ? (
                <Alert
                    type="error"
                    message={errorMessage}
                    style={{ margin: '24px 0' }}
                />
            ) : null}

            <Table
                rowKey={(record) => record.id}
                columns={columns}
                dataSource={categories}
                pagination={false}
                loading={{
                    spinning: loadingMessage !== '',
                    tip: loadingMessage,
                }}
            />
        </>
    );
};

export default CategoriesGrid;

import { useContext, useEffect, useState } from 'react';
import { Typography, Space, Button } from 'antd';
import AppContext from '@contexts/AppContext';
import { Api } from '@utils/api';
import { Category } from '@models/Category';
import CategoriesGrid from './CategoriesGrid';
import CategoryForm from './CategoryForm';

const { Title } = Typography;

const ManageCategories = (): JSX.Element => {
    const [loadingMessage, setLoadingMessage] = useState<string>('Loading...');
    const [formOpen, setFormOpen] = useState<boolean>(false);
    const [categories, setCategories] = useState<Category[]>([]);
    const [categoryToEditId, setCategoryToEditId] = useState<number>(0);

    const { siteSettings } = useContext(AppContext);

    const loadCategories = async () => {
        setLoadingMessage('Loading...');

        const [data, error] = await Api.Get<Category[]>('categories/getall');

        if (error || data === null) {
            setLoadingMessage('');
            return;
        }

        setCategories(data);
        setLoadingMessage('');
    };

    useEffect(() => {
        document.title = `Manage Categories - ${siteSettings.title}`;

        loadCategories();
    }, []);

    return (
        <>
            <Title level={1}>Categories</Title>

            <Space direction="vertical" size={24} className="full-width">
                <Button
                    type="primary"
                    onClick={() => setFormOpen(true)}
                >
                    Add Category
                </Button>

                <CategoriesGrid
                    categories={categories}
                    loadingMessage={loadingMessage}
                    onCategoryChanged={() => loadCategories()}
                    onEditCategory={(id) => {
                        setCategoryToEditId(id);
                        setFormOpen(true);
                    }}
                />
            </Space>

            <CategoryForm
                onSave={() => {
                    loadCategories();
                    setFormOpen(false);
                    setCategoryToEditId(0);
                }}
                onClose={() => {
                    setFormOpen(false);
                    setCategoryToEditId(0);
                }}
                currentCategories={categories}
                visible={formOpen}
                id={categoryToEditId}
            />
        </>
    );
};

export default ManageCategories;

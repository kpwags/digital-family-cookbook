import { useContext, useEffect, useState } from 'react';
import { Typography, Space, Button } from 'antd';
import AppContext from '@contexts/AppContext';
import { Api } from '@utils/api';
import { Meat } from '@models/Meat';
import MeatsGrid from './MeatsGrid';
import MeatForm from './MeatForm';

const { Title } = Typography;

const ManageCategories = (): JSX.Element => {
    const [loadingMessage, setLoadingMessage] = useState<string>('Loading...');
    const [formOpen, setFormOpen] = useState<boolean>(false);
    const [meats, setMeats] = useState<Meat[]>([]);
    const [meatToEditId, setMeatToEditId] = useState<number>(0);

    const { siteSettings, updateMeats } = useContext(AppContext);

    const loadMeats = async (doUpdateMeats = false) => {
        setLoadingMessage('Loading...');

        const [data, error] = await Api.Get<Meat[]>('meats/getall');

        if (error || data === null) {
            setLoadingMessage('');
            return;
        }

        setMeats(data);
        setLoadingMessage('');

        if (doUpdateMeats) {
            updateMeats(data);
        }
    };

    useEffect(() => {
        document.title = `Manage Meats - ${siteSettings.title}`;

        loadMeats();
    }, []);

    return (
        <>
            <Title level={1}>Meats</Title>

            <Space direction="vertical" size={24} className="full-width">
                <Button
                    type="primary"
                    onClick={() => setFormOpen(true)}
                >
                    Add Meat
                </Button>

                <MeatsGrid
                    meats={meats}
                    loadingMessage={loadingMessage}
                    onMeatChanged={() => loadMeats()}
                    onEditMeat={(id) => {
                        setMeatToEditId(id);
                        setFormOpen(true);
                    }}
                />
            </Space>

            <MeatForm
                onSave={() => {
                    loadMeats(true);
                    setFormOpen(false);
                    setMeatToEditId(0);
                }}
                onClose={() => {
                    setFormOpen(false);
                    setMeatToEditId(0);
                }}
                currentMeats={meats}
                visible={formOpen}
                id={meatToEditId}
            />
        </>
    );
};

export default ManageCategories;

import { useState } from 'react';
import {
    Table,
    Button,
    Alert,
} from 'antd';
import { ColumnsType } from 'antd/lib/table';
import ConfirmDialog from '@components/ConfirmDialog';
import { Api } from '@utils/api';
import { Meat } from '@models/Meat';

type MeatsGridProps = {
    meats: Meat[]
    loadingMessage: string
    onEditMeat: (id: number) => void
    onMeatChanged: () => void
}

const MeatsGrid = ({
    meats = [],
    loadingMessage = '',
    onEditMeat,
    onMeatChanged,
}: MeatsGridProps): JSX.Element => {
    const [errorMessage, setErrorMessage] = useState<string>('');

    const deleteMeat = async (id: number) => {
        const [, error] = await Api.Delete('meats/delete', { params: { id } });

        if (error) {
            setErrorMessage(error);
            return;
        }

        onMeatChanged();
    };

    const columns: ColumnsType<Meat> = [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'name',
            width: '80%',
        },
        {
            key: 'actions',
            title: 'Actions',
            render: (_, meat: Meat) => (
                <>
                    <Button type="link" onClick={() => onEditMeat(meat.meatId)}>Edit</Button>
                    <ConfirmDialog
                        onConfirm={() => { deleteMeat(meat.meatId); }}
                        text={`Are you sure you want to delete ${meat.name}?`}
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
                dataSource={meats}
                pagination={false}
                loading={{
                    spinning: loadingMessage !== '',
                    tip: loadingMessage,
                }}
            />
        </>
    );
};

export default MeatsGrid;

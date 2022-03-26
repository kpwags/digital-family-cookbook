import { useContext, useState } from 'react';
import {
    Table,
    Button,
    Alert,
} from 'antd';
import { ColumnsType } from 'antd/lib/table';
import ConfirmDialog from '@components/ConfirmDialog';
import { Api } from '@utils/api';
import AppContext from '@contexts/AppContext';
import { RoleType } from '@models/RoleType';

type RolesTableProps = {
    roles: RoleType[]
    loadingMessage: string
    onRoleChanged: () => void
    onRoleEdit: (id: string) => void
}

const RolesTable = ({
    roles = [],
    loadingMessage = '',
    onRoleChanged,
    onRoleEdit,
}: RolesTableProps): JSX.Element => {
    const [errorMessage, setErrorMessage] = useState<string>('');

    const { token } = useContext(AppContext);

    const deleteRole = async (id: string) => {
        const [, error] = await Api.Post('system/deleterole', { data: { id }, token });

        if (error) {
            setErrorMessage(error);
            return;
        }

        onRoleChanged();
    };

    const columns: ColumnsType<RoleType> = [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'name',
            width: '80%',
        },
        {
            key: 'actions',
            title: 'Actions',
            render: (_, role: RoleType) => (
                <>
                    <Button type="link" onClick={() => onRoleEdit(role.id)}>Edit</Button>
                    <ConfirmDialog
                        onConfirm={() => { deleteRole(role.id); }}
                        text={`Are you sure you want to delete ${role.name}?`}
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
                dataSource={roles}
                pagination={false}
                loading={{
                    spinning: loadingMessage !== '',
                    tip: loadingMessage,
                }}
            />
        </>
    );
};

export default RolesTable;

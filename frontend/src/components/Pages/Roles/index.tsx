import { AppContext } from '@contexts/AppContext';
import { Api } from '@lib/api';
import { RoleType } from '@models/RoleType';
import { useEffect, useState, useContext } from 'react';
import { RoleForm } from '@components/Forms/RoleForm';
import {
    Button,
    Space,
    Alert,
    Typography,
} from 'antd';
import { WarningOutlined } from '@ant-design/icons';
import { RolesTable } from './components/RolesTable';

const { Text, Title } = Typography;

const Roles = (): JSX.Element => {
    const [roles, setRoles] = useState<RoleType[]>([]);
    const [rolesFormOpen, setRolesFormOpen] = useState<boolean>(false);
    const [rolesLoadingMessage, setRolesLoadingMessage] = useState<string>('Loading...');
    const [roleToEditId, setRoleToEditId] = useState<string>('');

    const { siteSettings, token } = useContext(AppContext);

    const fetchRoles = async () => {
        setRolesLoadingMessage('Loading...');

        const [data, error] = await Api.Get<RoleType[]>('system/getroles', { token });

        if (error || data === null) {
            setRolesLoadingMessage('');
            return;
        }

        setRoles(data);
        setRolesLoadingMessage('');
    };

    useEffect(() => {
        document.title = `Manage Roles - ${siteSettings.title}`;
        fetchRoles();
    }, []);

    return (
        <>
            <Title level={1}>Roles</Title>

            <Alert
                type="warning"
                message={<Text><WarningOutlined />&nbsp;<Text strong>WARNING:</Text> Adjusting roles can cause the site to break.</Text>}
                style={{ margin: '24px 0' }}
            />

            <Space direction="vertical" size={24} className="full-width">
                <Button
                    type="primary"
                    onClick={() => setRolesFormOpen(true)}
                >
                    Add Role
                </Button>

                <RolesTable
                    roles={roles}
                    onRoleEdit={(id) => {
                        setRoleToEditId(id);
                        setRolesFormOpen(true);
                    }}
                    loadingMessage={rolesLoadingMessage}
                    onRoleChanged={() => fetchRoles()}
                />
            </Space>

            <RoleForm
                onSave={() => {
                    fetchRoles();
                    setRolesFormOpen(false);
                    setRoleToEditId('');
                }}
                onClose={() => {
                    setRolesFormOpen(false);
                    setRoleToEditId('');
                }}
                currentRoles={roles}
                visible={rolesFormOpen}
                id={roleToEditId}
            />
        </>
    );
};

export { Roles };

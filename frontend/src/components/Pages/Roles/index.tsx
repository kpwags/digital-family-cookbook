import { AppContext } from '@contexts/AppContext';
import { Api } from '@lib/api';
import { RoleType } from '@models/RoleType';
import { useEffect, useState, useContext } from 'react';
import { RoleForm } from '@components/Forms/RoleForm';
import { Button, Space } from 'antd';
import { RolesTable } from './components/RolesTable';

const Roles = () => {
    const [roles, setRoles] = useState<RoleType[]>([]);
    const [rolesFormOpen, setRolesFormOpen] = useState<boolean>(false);

    const { siteSettings, token } = useContext(AppContext);

    const fetchRoles = async () => {
        const [data, error] = await Api.Get<RoleType[]>('system/getroles', { token });

        if (error || data === null) {
            return;
        }

        setRoles(data);
    };

    useEffect(() => {
        document.title = `Manage Roles - ${siteSettings.title}`;
        fetchRoles();
    }, []);

    return (
        <>
            <h1>Roles</h1>
            <Space direction="vertical" size={24} className="full-width">
                <Button
                    type="primary"
                    onClick={() => setRolesFormOpen(true)}
                >
                    Add Role
                </Button>

                <RolesTable roles={roles} />
            </Space>

            <RoleForm
                onSave={() => {
                    fetchRoles();
                    setRolesFormOpen(false);
                }}
                onClose={() => setRolesFormOpen(false)}
                visible={rolesFormOpen}
                id=""
            />
        </>
    );
};

export { Roles };

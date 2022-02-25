import { useContext, useState, useEffect } from 'react';
import {
    Table,
    Button,
    Alert,
    Space,
} from 'antd';
import { ColumnsType } from 'antd/lib/table';
import { ConfirmDialog } from '@components/ConfirmDialog';
import { Api } from '@lib/api';
import { UserAccount } from '@models/UserAccount';
import { AppContext } from '@contexts/AppContext';

const Users = (): JSX.Element => {
    const [users, setUsers] = useState<UserAccount[]>([]);
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [loadingMessage, setLoadingMessage] = useState<string>('Loading...');

    const { user, token } = useContext(AppContext);

    const fetchUsers = async () => {
        const [data, error] = await Api.Get<UserAccount[]>('system/getusers', { params: { includeRoles: true }, token });

        if (error || data === null) {
            setErrorMessage(error || 'An error has occurred');
            setLoadingMessage('');
            return;
        }

        setUsers(data);
        setLoadingMessage('');
    };

    const deleteUser = async (id: string) => {
        setLoadingMessage('Deleting User...');

        const [, error] = await Api.Post('system/deleteuser', { data: { id }, token });

        if (error) {
            setErrorMessage(error);
            return;
        }

        fetchUsers();
    };

    const makeAdministrator = async (userId: string) => {
        setLoadingMessage('Updating Roles...');

        const [, error] = await Api.Post(
            'system/addusertorole',
            {
                data: {
                    userAccountId: userId,
                    roleName: 'Administrator',
                },
                token,
            },
        );

        if (error) {
            setErrorMessage(error);
            return;
        }

        fetchUsers();
    };

    const removeAdministrator = async (userId: string) => {
        setLoadingMessage('Updating Roles...');

        const [, error] = await Api.Post(
            'system/deleterolefromuser',
            {
                data: {
                    userAccountId: userId,
                    roleName: 'Administrator',
                },
                token,
            },
        );

        if (error) {
            setErrorMessage(error);
            return;
        }

        fetchUsers();
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    const columns: ColumnsType<UserAccount> = [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'name',
            width: '50%',
        },
        {
            key: 'roles',
            title: 'Roles',
            dataIndex: 'roles',
            width: '30%',
            render: (_, user: UserAccount) => (
                <>{user.roles.map((r) => r.name).join(', ')}</>
            ),
        },
        {
            key: 'actions',
            title: 'Actions',
            render: (_, userAccount: UserAccount) => (
                <Space direction="horizontal" size={4}>
                    {userAccount.id !== user?.id ? (
                        <>
                            <>
                                {userAccount.roles.map((r) => r.normalizedName).includes('ADMINISTRATOR') ? (
                                    <Button
                                        type="link"
                                        onClick={() => removeAdministrator(userAccount.id)}
                                    >
                                        Remove Admin
                                    </Button>
                                ) : (
                                    <Button
                                        type="link"
                                        onClick={() => makeAdministrator(userAccount.id)}
                                    >
                                        Make Admin
                                    </Button>
                                )}
                            </>
                            <ConfirmDialog
                                onConfirm={() => { deleteUser(userAccount.id); }}
                                text={`Are you sure you want to delete ${userAccount.name}?`}
                            >
                                <Button type="link">Delete</Button>
                            </ConfirmDialog>
                        </>
                    ) : null}
                </Space>
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
                dataSource={users}
                pagination={false}
                loading={{
                    spinning: loadingMessage !== '',
                    tip: loadingMessage,
                }}
            />
        </>
    );
};

export { Users };

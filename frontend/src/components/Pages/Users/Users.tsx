import { useContext, useEffect } from 'react';
import { Typography } from 'antd';
import AppContext from '@contexts/AppContext';
import UsersGrid from './UsersGrid';

const { Title } = Typography;

const Users = (): JSX.Element => {
    const { siteSettings } = useContext(AppContext);

    useEffect(() => {
        document.title = `Users - ${siteSettings.title}`;
    }, []);

    return (
        <>
            <Title level={1}>Users</Title>
            <UsersGrid />
        </>
    );
};

export default Users;

import { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Layout, Menu } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import AppContext from '@contexts/AppContext';
import { hasRole } from '@utils/UserFunctions';

import './NavBar.less';
import { Category } from '@models/Category';
import { Api } from '@utils/api';

const { Header } = Layout;
const { SubMenu } = Menu;

type NavBarProps = {
    selectedItem?: string
}

const NavBar = ({
    selectedItem = '',
}: NavBarProps): JSX.Element => {
    const [categories, setCategories] = useState<Category[]>([]);

    const loadCategories = async () => {
        const [data, error] = await Api.Get<Category[]>('categories/getall');

        if (error || data === null) {
            return;
        }

        setCategories(data);
    };

    const {
        siteSettings,
        user,
        logout,
    } = useContext(AppContext);

    useEffect(() => {
        loadCategories();
    }, []);

    return (
        <Header className="nav">
            <div className="logo">
                <Link to="/">{siteSettings.title}</Link>
            </div>

            <Menu theme="dark" mode="horizontal" selectedKeys={[selectedItem]} className="nav-bar-menu">
                <SubMenu key="categories-dropdown" title="Categories">
                    {categories.length > 0 ? categories.map((c) => (
                        <Menu.Item key={`categories-${c.categoryId}`}>
                            <Link to="/">{c.name}</Link>
                        </Menu.Item>
                    )) : (
                        <Menu.Item key="no-categories">No Categories</Menu.Item>
                    )}
                </SubMenu>
                <Menu.Item key="meats">Meats</Menu.Item>
                {user && user.id !== '' ? (
                    <SubMenu key="user-dropdown" icon={<UserOutlined />} title={user.name}>
                        {hasRole(user, 'ADMINISTRATOR') ? (
                            <Menu.Item key="manage-users">
                                <Link to="/manage-users">Manage Users</Link>
                            </Menu.Item>
                        ) : null}

                        {hasRole(user, 'ADMINISTRATOR') ? (
                            <Menu.Item key="manage-categories">
                                <Link to="/manage-categories">Manage Categories</Link>
                            </Menu.Item>
                        ) : null}

                        {hasRole(user, 'ADMINISTRATOR') ? (
                            <Menu.Item key="site-settings">
                                <Link to="/site-settings">Edit Site Settings</Link>
                            </Menu.Item>
                        ) : null}

                        <Menu.Item key="logout" onClick={logout}>Log Out</Menu.Item>
                    </SubMenu>
                ) : (
                    <>
                        <Menu.Item key="register">
                            <Link to="/register">Register</Link>
                        </Menu.Item>
                        <Menu.Item key="login">
                            <Link to="/login">Sign In</Link>
                        </Menu.Item>
                    </>
                )}
            </Menu>
        </Header>
    );
};

export default NavBar;

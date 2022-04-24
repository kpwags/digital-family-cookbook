import { useContext } from 'react';
import { Link } from 'react-router-dom';
import { Layout, Menu } from 'antd';
import { FileAddOutlined, UserOutlined } from '@ant-design/icons';
import AppContext from '@contexts/AppContext';
import { hasRole } from '@utils/UserFunctions';

import './NavBar.less';

const { Header } = Layout;
const { SubMenu } = Menu;

type NavBarProps = {
    selectedItem?: string
}

const NavBar = ({
    selectedItem = '',
}: NavBarProps): JSX.Element => {
    const {
        siteSettings,
        categories,
        meats,
        user,
        logout,
    } = useContext(AppContext);

    return (
        <Header className="nav">
            <div className="logo">
                <Link to="/">{siteSettings.title}</Link>
            </div>

            <Menu theme="dark" mode="horizontal" selectedKeys={[selectedItem]} className="nav-bar-menu">
                <Menu.Item key="add-recipe" icon={<FileAddOutlined />}>
                    <Link to="/recipes/add">New Recipe</Link>
                </Menu.Item>
                <SubMenu key="categories-dropdown" title="Categories">
                    {categories.length > 0 ? categories.map((c) => (
                        <Menu.Item key={`categories-${c.categoryId}`}>
                            <Link to="/">{c.name}</Link>
                        </Menu.Item>
                    )) : (
                        <Menu.Item key="no-categories">No Categories</Menu.Item>
                    )}
                </SubMenu>
                <SubMenu key="meats-dropdown" title="Meats">
                    {meats.length > 0 ? meats.map((m) => (
                        <Menu.Item key={`meats-${m.meatId}`}>
                            <Link to="/">{m.name}</Link>
                        </Menu.Item>
                    )) : (
                        <Menu.Item key="no-meats">No Meats</Menu.Item>
                    )}
                </SubMenu>
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
                            <Menu.Item key="manage-meats">
                                <Link to="/manage-meats">Manage Meats</Link>
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

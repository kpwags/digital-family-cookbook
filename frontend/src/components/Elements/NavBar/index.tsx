import { useContext } from 'react';
import { Link } from 'react-router-dom';
import { Layout, Menu } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import { AppContext } from '@contexts/AppContext';
import { hasRole } from '@lib/UserFunctions';

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
        user,
        logout,
    } = useContext(AppContext);

    return (
        <Header className="nav">
            <div className="logo">
                <Link to="/">{siteSettings.title}</Link>
            </div>

            <Menu theme="dark" mode="horizontal" selectedKeys={[selectedItem]} className="nav-bar-menu">
                <Menu.Item key="categories">Categories</Menu.Item>
                <Menu.Item key="meats">Meats</Menu.Item>
                {user && user.id !== '' ? (
                    <SubMenu key="user-dropdown" icon={<UserOutlined />} title={user.name}>
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

export { NavBar };

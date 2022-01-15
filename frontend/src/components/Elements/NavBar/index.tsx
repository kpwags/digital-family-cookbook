import { Layout, Menu } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import './NavBar.css';

const { Header } = Layout;

type NavBarProps = {
    selectedItem?: string
}

const NavBar = ({
    selectedItem = '',
}: NavBarProps): JSX.Element => {
    const isUserLoggedIn = false;

    return (
        <Header className="nav">
            <div className="logo">Digital Family Cookbook</div>
            <Menu theme="dark" mode="horizontal" selectedKeys={[selectedItem]} className="nav-bar-menu">
                <Menu.Item key="categories">Categories</Menu.Item>
                <Menu.Item key="meats">Meats</Menu.Item>
                {isUserLoggedIn ? (
                    <Menu.Item key="1" icon={<UserOutlined />} className="user-menu">
                        User
                    </Menu.Item>
                ) : (
                    <>
                        <Menu.Item key="register">Register</Menu.Item>
                        <Menu.Item key="login">Sign In</Menu.Item>
                    </>
                )}
            </Menu>
        </Header>
    );
};

export { NavBar };

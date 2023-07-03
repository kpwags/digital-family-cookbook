import { useContext } from 'react';
import { Link } from 'react-router-dom';
import { Layout, Menu } from 'antd';
import {
    PlusOutlined,
    MenuOutlined,
    UserOutlined,
    SearchOutlined,
} from '@ant-design/icons';
import QuickSearchForm from '@components/Forms/QuickSearchForm';
import AppContext from '@contexts/AppContext';
import { hasRole } from '@utils/UserFunctions';
import { ItemType } from 'antd/lib/menu/hooks/useItems';

import './NavBar.less';

const { Header } = Layout;

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

    const showForAnonymousUser = siteSettings.isPublic || (user && user.id !== '');

    const buildMenu = (): ItemType[] => {
        const menuItems: ItemType[] = [];

        if (showForAnonymousUser) {
            menuItems.push({
                key: 'search-form',
                className: 'quicksearch-form',
                label: <QuickSearchForm />,
            });

            menuItems.push({
                key: 'search-recipes',
                className: 'search-link',
                icon: <SearchOutlined />,
                label: <Link to="/search">Search</Link>,
            });
        }

        if (user && user.id !== '') {
            menuItems.push({
                key: 'add-recipe',
                icon: <PlusOutlined />,
                label: <Link to="/recipes/add">New Recipe</Link>,
            });
        }

        if (showForAnonymousUser) {
            menuItems.push({
                key: 'all-recipes',
                label: <Link to="/recipes/list">All Recipes</Link>,
            });
        }

        if (user && user.id !== '') {
            menuItems.push({
                key: 'favorite-recipes',
                label: <Link to="/recipes/favorites">Favorites</Link>,
            });
        }

        if (showForAnonymousUser) {
            menuItems.push({
                key: 'categories',
                label: 'Categories',
                children: categories.length === 0
                    ? [{
                        key: 'no-categories',
                        label: <>No Categories</>,
                    }]
                    : categories.map((c) => ({
                        key: `category-${c.categoryId}`,
                        label: <Link to={`/recipes/category/${c.categoryId}`}>{c.name}</Link>,
                    })),
            });

            menuItems.push({
                key: 'meats',
                label: 'Meats',
                children: meats.length === 0
                    ? [{
                        key: 'no-meats',
                        label: <>No Meats</>,
                    }]
                    : meats.map((m) => ({
                        key: `meat-${m.meatId}`,
                        label: <Link to={`/recipes/meat/${m.meatId}`}>{m.name}</Link>,
                    })),
            });
        }

        if (user && user.id !== '') {
            const userMenuItems: ItemType[] = [];

            userMenuItems.push({
                key: 'manage-recipes',
                label: <Link to="/manage-recipes">Manage Recipes</Link>,
            });

            if (hasRole(user, 'ADMINISTRATOR')) {
                userMenuItems.push({
                    key: 'manage-users',
                    label: <Link to="/manage-users">Manage Users</Link>,
                });

                userMenuItems.push({
                    key: 'manage-categories',
                    label: <Link to="/manage-categories">Manage Categories</Link>,
                });

                userMenuItems.push({
                    key: 'manage-meats',
                    label: <Link to="/manage-meats">Manage Meats</Link>,
                });

                userMenuItems.push({
                    key: 'site-settings',
                    label: <Link to="/site-settings">Edit Site Settings</Link>,
                });
            }

            userMenuItems.push({
                key: 'logout',
                label: <>Log Out</>,
                onClick: () => logout(),
            });

            menuItems.push({
                key: 'user',
                icon: <UserOutlined />,
                label: user.name,
                children: userMenuItems,
            });
        } else {
            menuItems.push({
                key: 'register',
                label: <Link to="/register">Register</Link>,
            });

            menuItems.push({
                key: 'signin',
                label: <Link to="/login">Sign In</Link>,
            });
        }

        return menuItems;
    };

    return (
        <Header className="nav">
            <div className="logo">
                <Link to="/">{siteSettings.title}</Link>
            </div>

            <Menu
                theme="dark"
                mode="horizontal"
                selectedKeys={[selectedItem]}
                className="nav-bar-menu"
                items={buildMenu()}
                overflowedIndicator={<MenuOutlined />}
            />
        </Header>
    );
};

export default NavBar;

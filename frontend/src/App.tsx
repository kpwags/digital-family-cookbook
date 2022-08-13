import MainApp from '@components/MainApp';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import BaseLayout from '@components/Elements/BaseLayout';
import { CookiesProvider } from 'react-cookie';
import ProtectedRoute from '@components/ProtectedRoute';

import Landing from '@components/Pages/Landing';
import Register from '@components/Pages/Register';
import Login from '@components/Pages/Login';
import Roles from '@components/Pages/Roles';
import Users from '@components/Pages/Users';
import SiteSettings from '@components/Pages/SiteSettings';
import ManageCategories from '@components/Pages/ManageCategories';
import AddRecipe from '@components/Pages/AddRecipe';
import EditRecipe from '@components/Pages/EditRecipe';
import ViewRecipe from '@components/Pages/ViewRecipe';
import ManageMeats from '@components/Pages/ManageMeats';
import ManageRecipes from '@components/Pages/ManageRecipes';
import RecipesByCategory from '@components/Pages/RecipesByCategory';

import './styles/App.less';

const App = (): JSX.Element => (
    <CookiesProvider>
        <BrowserRouter>
            <MainApp>
                <BaseLayout>
                    <Routes>
                        <Route path="/register" element={<Register />} />
                        <Route path="/login" element={<Login />} />
                        <Route
                            path="/roles"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR']}>
                                    <Roles />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/manage-users"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR']}>
                                    <Users />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/manage-categories"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR']}>
                                    <ManageCategories />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/manage-meats"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR']}>
                                    <ManageMeats />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/manage-recipes"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR', 'USER']}>
                                    <ManageRecipes />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/site-settings"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR']}>
                                    <SiteSettings />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/recipes/add"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR', 'USER']}>
                                    <AddRecipe />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/recipes/edit/:id"
                            element={(
                                <ProtectedRoute requiredRoles={['ADMINISTRATOR', 'USER']}>
                                    <EditRecipe />
                                </ProtectedRoute>
                            )}
                        />
                        <Route
                            path="/recipes/view/:id"
                            element={(
                                <ViewRecipe />
                            )}
                        />
                        <Route
                            path="/recipes/category/:id"
                            element={(
                                <RecipesByCategory />
                            )}
                        />
                        <Route path="/" element={<Landing />} />
                    </Routes>
                </BaseLayout>
            </MainApp>
        </BrowserRouter>
    </CookiesProvider>
);

export default App;

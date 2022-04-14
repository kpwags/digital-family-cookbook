import { screen } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount, MockUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import ProtectedRoute from '@components/ProtectedRoute';

describe('<ProtectedRoute />', () => {
    test('It allows the user in when they are logged in when no roles are specified', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <ProtectedRoute>
                    <p>Access Granted</p>
                </ProtectedRoute>
            </MockAppProvider>,
        );

        await screen.findByText(/Access Granted/);
    });

    test('It shows the login form when no user is logged in', async () => {
        renderWithRouter(
            <MockAppProvider>
                <ProtectedRoute>
                    <p>Access Granted</p>
                </ProtectedRoute>
            </MockAppProvider>,
        );

        await screen.findByText(/Enter your email and password to gain access./);
        expect(screen.queryByText(/Access Granted/)).not.toBeInTheDocument();
    });

    test('It allows the user in when they are logged in with appropriate role', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <ProtectedRoute requiredRoles={['ADMINISTRATOR']}>
                    <p>Access Granted</p>
                </ProtectedRoute>
            </MockAppProvider>,
        );

        await screen.findByText(/Access Granted/);
    });

    test('It does not allow a user in without the appropriate role', async () => {
        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <ProtectedRoute requiredRoles={['ADMINISTRATOR']}>
                    <p>Access Granted</p>
                </ProtectedRoute>
            </MockAppProvider>,
        );

        await screen.findByText(/No Access/);
        expect(screen.queryByText(/Access Granted/)).not.toBeInTheDocument();
    });
});

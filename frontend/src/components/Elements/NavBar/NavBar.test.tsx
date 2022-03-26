import {
    screen,
} from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import NavBar from '.';

describe('<NavBar />', () => {
    test('it does shows the sign in and register button when the user is not logged in', async () => {
        renderWithRouter(
            <MockAppProvider>
                <NavBar />
            </MockAppProvider>,
        );

        await screen.findByText('Sign In');
        await screen.findByText('Register');
    });

    test('it does shows the users name when logged in', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <NavBar />
            </MockAppProvider>,
        );

        await screen.findByText('Admin User');

        expect(screen.queryByText('Sign In')).not.toBeInTheDocument();
        expect(screen.queryByText('Register')).not.toBeInTheDocument();
    });
});

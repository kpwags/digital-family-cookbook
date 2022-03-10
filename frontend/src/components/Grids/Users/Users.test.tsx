import { screen, waitForElementToBeRemoved } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import { Users } from '.';

describe('<Users />', () => {
    test('It renders the grid', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <Users />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        await screen.findByText(/Admin User/);
        await screen.findByText(/Regular User/);

        const deleteButtons = await screen.findAllByText(/Delete/);

        expect(deleteButtons.length).toBe(1);
    });

    test('It confirms the user wants to delete a user', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <Users />
            </MockAppProvider>,
        );

        const deleteButton = await screen.findByRole('button', { name: /Delete/ });

        userEvent.click(deleteButton);

        await screen.findAllByText('Are you sure you want to delete Regular User?');
    });
});

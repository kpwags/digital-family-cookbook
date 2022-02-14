import { screen, waitForElementToBeRemoved } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import { Roles } from '.';

describe('<Roles />', () => {
    test('It renders the grid for roles', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <Roles />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        await screen.findByText(/Adjusting roles can cause the site to break./);

        await screen.findByText(/Administrator/);
        await screen.findByText(/User/);

        expect(screen.queryAllByText(/Edit/).length).toBe(2);
        expect(screen.queryAllByText(/Delete/).length).toBe(2);
    });

    test('It confirms before deleting a role', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <Roles />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const deleteButtons = await screen.findAllByText(/Delete/);

        userEvent.click(deleteButtons[0]);

        await screen.findByText(/Are you sure you want to delete Administrator?/);
    });

    test("The 'Add Role' Button opens the form", async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <Roles />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const addButton = await screen.findByRole('button', { name: 'Add Role' });

        userEvent.click(addButton);

        await screen.findByLabelText(/Name/);
    });

    test('It opens the edit role form', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <Roles />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const editButtons = await screen.findAllByText(/Edit/);

        userEvent.click(editButtons[0]);

        await screen.findByText(/Edit Role/);
    });
});

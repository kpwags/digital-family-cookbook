import { screen, waitForElementToBeRemoved, act } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import { RoleForm } from '.';

describe('<RoleForm />', () => {
    test('It renders the form for a new role', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id=""
                    visible
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;

        expect(nameField.value).toBe('');
    });

    test('It renders the form for a new role', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id="admin"
                    visible
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;

        expect(nameField.value).toBe('Administrator');
    });

    test('It shows an error when a role is not found', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id="notfound"
                    visible
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        await screen.findByText(/Unable to find role/);
    });

    test('It will not let a role be saved without a name', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id=""
                    visible
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        const saveButton = await screen.findByRole('button', { name: 'Save' });

        userEvent.click(saveButton);

        await screen.findByText(/Name is required/);

        expect(mockSuccess).not.toBeCalled();
    });

    test('It will not save when the cancel button is clicked', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id=""
                    visible
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        const cancelButton = await screen.findByRole('button', { name: 'Cancel' });

        userEvent.click(cancelButton);

        expect(mockSuccess).not.toBeCalled();
        expect(mockClose).toBeCalled();
    });

    test('It successfully saves a role', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id=""
                    visible
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;
        const saveButton = await screen.findByRole('button', { name: 'Save' });

        await act(async () => {
            await userEvent.clear(nameField);
            await userEvent.type(nameField, 'Asst. to the Regional Manager');

            userEvent.click(saveButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Saving.../));

        expect(mockSuccess).toHaveBeenCalledTimes(1);
    });

    test('It alerts the user saving a new role that already exists (client side validation)', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id=""
                    visible
                    currentRoles={[
                        {
                            id: '1234',
                            name: 'Administrator',
                            normalizedName: 'ADMINISTRATOR',
                            roleTypeId: '1234',
                        },
                        {
                            id: '5678',
                            name: 'User',
                            normalizedName: 'USER',
                            roleTypeId: '5678',
                        },
                    ]}
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;
        const saveButton = await screen.findByRole('button', { name: 'Save' });

        await act(async () => {
            await userEvent.clear(nameField);
            await userEvent.type(nameField, 'Administrator');

            userEvent.click(saveButton);
        });

        await screen.findByText("A role with the name 'Administrator' already exists.");

        expect(mockSuccess).not.toBeCalled();
        expect(mockClose).not.toBeCalled();
    });

    test('It errors saving a new role that already exists', async () => {
        const mockSuccess = jest.fn();
        const mockClose = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RoleForm
                    id=""
                    visible
                    onSave={mockSuccess}
                    onClose={mockClose}
                />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;
        const saveButton = await screen.findByRole('button', { name: 'Save' });

        await act(async () => {
            await userEvent.clear(nameField);
            await userEvent.type(nameField, 'Administrator');

            userEvent.click(saveButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Saving.../));

        await screen.findByText(/Role already exists/);

        expect(mockSuccess).not.toBeCalled();
        expect(mockClose).not.toBeCalled();
    });
});

import { act, screen, waitForElementToBeRemoved } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import MeatForm from '@components/Pages/ManageMeats/MeatForm';

describe('<MeatForm />', () => {
    test('It validates a new meat', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatForm
                    visible
                    onSave={() => jest.fn()}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        const saveButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.click(saveButton);

        await screen.findByText(/Name is required/);
    });

    test("It validates a new meat to ensure it does't already exist", async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatForm
                    visible
                    currentMeats={[
                        {
                            id: '123',
                            name: 'meat',
                            meatId: 1,
                        },
                    ]}
                    onSave={() => jest.fn()}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        const nameField = screen.getByLabelText(/Name/);

        userEvent.type(nameField, 'meat');

        await act(async () => {
            await userEvent.click(screen.getByRole('button', { name: /Save/ }));
        });

        expect(screen.queryByText("A meat with the name 'meat' already exists.")).toBeInTheDocument();
    });

    test('It saves a new meat', async () => {
        const mockOnSave = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatForm
                    visible
                    onSave={mockOnSave}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        const nameField = screen.getByLabelText(/Name/) as HTMLInputElement;

        const saveButton = screen.getByRole('button', { name: /Save/ });

        userEvent.clear(nameField);
        userEvent.type(nameField, 'Fake Meat');

        await act(async () => {
            await userEvent.click(saveButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Saving.../));

        expect(mockOnSave).toBeCalledTimes(1);
    });

    test('It loads an existing meat', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatForm
                    id={1}
                    visible
                    onSave={() => jest.fn()}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const nameField = screen.getByLabelText(/Name/) as HTMLInputElement;

        expect(nameField.value).toBe('Meat');
    });

    test('It validates an existing meat', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatForm
                    id={1}
                    visible
                    onSave={() => jest.fn()}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;

        const saveButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.clear(nameField);
        userEvent.click(saveButton);

        await screen.findByText(/Name is required/);
    });

    test('It saves an existing meat', async () => {
        const mockOnSave = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatForm
                    id={1}
                    visible
                    onSave={mockOnSave}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;

        const saveButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.clear(nameField);
        userEvent.type(nameField, 'Real Meat');

        await act(async () => {
            await userEvent.click(saveButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Saving.../));

        expect(mockOnSave).toBeCalledTimes(1);
    });
});

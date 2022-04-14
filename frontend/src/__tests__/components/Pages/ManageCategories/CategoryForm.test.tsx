import { act, screen, waitForElementToBeRemoved } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import CategoryForm from '@components/Pages/ManageCategories/CategoryForm';

describe('<CategoryForm />', () => {
    test('It validates a new category', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoryForm
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

    test("It validates a new category to ensure it does't already exist", async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoryForm
                    visible
                    currentCategories={[
                        {
                            id: '123',
                            name: 'meat',
                            categoryId: 1,
                        },
                    ]}
                    onSave={() => jest.fn()}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Name/);

        userEvent.type(nameField, 'meat');

        const saveButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.click(saveButton);

        await screen.findByText("A cateogry with the name 'meat' already exists.");
    });

    test('It saves a new category', async () => {
        const mockOnSave = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoryForm
                    visible
                    onSave={mockOnSave}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;

        const saveButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.clear(nameField);
        userEvent.type(nameField, 'Fake Meat');

        await act(async () => {
            await userEvent.click(saveButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Saving.../));

        expect(mockOnSave).toBeCalledTimes(1);
    });

    test('It loads an existing category', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoryForm
                    id={1}
                    visible
                    onSave={() => jest.fn()}
                    onClose={() => jest.fn()}
                />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;

        expect(nameField.value).toBe('Meat');
    });

    test('It validates an existing category', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoryForm
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

    test('It saves an existing category', async () => {
        const mockOnSave = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoryForm
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

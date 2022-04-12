import { screen, waitForElementToBeRemoved } from '@testing-library/react';
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

        const addButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.click(addButton);

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

        const addButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.click(addButton);

        await screen.findByText("A cateogry with the name 'meat' already exists.");
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

        const nameField = await screen.findByLabelText(/Name/) as HTMLInputElement;

        const addButton = await screen.findByRole('button', { name: /Save/ });

        userEvent.clear(nameField);
        userEvent.click(addButton);

        await screen.findByText(/Name is required/);
    });
});

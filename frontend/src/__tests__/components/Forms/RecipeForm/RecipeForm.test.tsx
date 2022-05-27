import {
    act,
    render,
    screen,
} from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import RecipeForm from '@components/Forms/RecipeForm';
import userEvent from '@testing-library/user-event';

describe('<RecipeForm />', () => {
    test('It loads the form with the default display', () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const ingredients = screen.getAllByTestId(/^ingredient-/);
        const directions = screen.getAllByTestId(/^direction-/);

        expect(ingredients.length).toBe(1);
        expect(directions.length).toBe(1);
    });

    test('Clicking the "Add Ingredient" button adds a new ingredient input field', () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const addIngredientButton = screen.getByRole('button', { name: 'Add Ingredient' });

        userEvent.click(addIngredientButton);

        const ingredients = screen.getAllByTestId(/^ingredient-/);

        expect(ingredients.length).toBe(2);
    });

    test('Clicking the "Add Step" button adds a new direction text field', () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const addStepButton = screen.getByRole('button', { name: 'Add Step' });

        userEvent.click(addStepButton);

        const directions = screen.getAllByTestId(/^direction-/);

        expect(directions.length).toBe(2);
    });

    test('Clicking the "Delete Ingredient" button removes the ingredient input field', () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const addIngredientButton = screen.getByRole('button', { name: 'Add Ingredient' });

        userEvent.click(addIngredientButton);
        userEvent.click(addIngredientButton);

        const deleteIngredientButton = screen.getByTestId(/delete-ingredient-2/);

        userEvent.click(deleteIngredientButton);

        const ingredients = screen.getAllByTestId(/^ingredient-/);

        expect(ingredients.length).toBe(2);

        expect(screen.queryByTestId(/^ingredient-1/)).toBeInTheDocument();
        expect(screen.queryByTestId(/^ingredient-2/)).not.toBeInTheDocument();
        expect(screen.queryByTestId(/^ingredient-3/)).toBeInTheDocument();
    });

    test('Clicking the "Delete Step" button direction the step input field', () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const addStepButton = screen.getByRole('button', { name: 'Add Step' });

        userEvent.click(addStepButton);
        userEvent.click(addStepButton);

        const deleteStepButton = screen.getByTestId(/delete-step-2/);

        userEvent.click(deleteStepButton);

        const directions = screen.getAllByTestId(/^direction-/);

        expect(directions.length).toBe(2);

        expect(screen.queryByTestId(/^direction-1/)).toBeInTheDocument();
        expect(screen.queryByTestId(/^direction-2/)).not.toBeInTheDocument();
        expect(screen.queryByTestId(/^direction-3/)).toBeInTheDocument();
    });

    test('It validates the recipe has a name', async () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        act(() => {
            const submitButton = screen.getByRole('button', { name: 'Save Recipe' });
            userEvent.click(submitButton);
        });

        await screen.findByText(/Name is required/);
    });

    test('It validates there is at least 1 ingredient', async () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const nameField = screen.getByLabelText('Name');
        userEvent.type(nameField, 'Delicious Recipe');

        act(() => {
            const submitButton = screen.getByRole('button', { name: 'Save Recipe' });
            userEvent.click(submitButton);
        });

        await screen.findByText(/Please enter an ingredient/);
    });

    test('It validates there is at least 1 step', async () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const nameField = screen.getByLabelText('Name');
        userEvent.type(nameField, 'Delicious Recipe');

        act(() => {
            const submitButton = screen.getByRole('button', { name: 'Save Recipe' });
            userEvent.click(submitButton);
        });

        await screen.findByText(/Please enter a step/);
    });
});

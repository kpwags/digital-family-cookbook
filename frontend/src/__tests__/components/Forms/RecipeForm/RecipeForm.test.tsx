import {
    act,
    screen,
} from '@testing-library/react';
import { renderWithRouter } from '@test/renderWithRouter';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import RecipeForm from '@components/Forms/RecipeForm';
import userEvent from '@testing-library/user-event';
import { MockRecipe } from '@test/mocks/MockRecipe';

describe('<RecipeForm />', () => {
    test('It loads the form with the default display', () => {
        renderWithRouter(
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
        renderWithRouter(
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
        renderWithRouter(
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
        renderWithRouter(
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
        renderWithRouter(
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
        renderWithRouter(
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
        renderWithRouter(
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

    test('It does not allow the user to delete the only ingredient', () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        expect(screen.queryByTestId(/delete-ingredient-/)).not.toBeVisible();
    });

    test('It adds a second ingredient to the form and either ingredient is now deletable', () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        act(() => {
            const addIngredientButton = screen.getByRole('button', { name: 'Add Ingredient' });
            userEvent.click(addIngredientButton);
        });

        expect(screen.queryAllByTestId(/^ingredient-/).length).toBe(2);
        expect(screen.queryAllByTestId(/^delete-ingredient-/).length).toBe(2);
    });

    test('It validates there is at least 1 step', async () => {
        renderWithRouter(
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

    test('It does not allow the user to delete the only step', () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        expect(screen.queryByTestId(/delete-step-/)).not.toBeVisible();
    });

    test('It adds a second step to the form and either step is now deletable', () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        act(() => {
            const addStepButton = screen.getByRole('button', { name: 'Add Step' });
            userEvent.click(addStepButton);
        });

        expect(screen.queryAllByTestId(/^direction-/).length).toBe(2);
        expect(screen.queryAllByTestId(/^delete-step-/).length).toBe(2);
    });

    test('It loads an existing recipe', async () => {
        const recipe = MockRecipe();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm
                    recipe={recipe}
                    onSave={jest.fn()}
                />
            </MockAppProvider>,
        );

        const nameField = screen.getByLabelText(/Name/) as HTMLInputElement;
        expect(nameField.value).toBe(recipe.name);

        const servingsField = screen.getByLabelText(/Servings/) as HTMLInputElement;
        expect(servingsField.value).toBe(recipe.servings?.toString());

        const sourceField = screen.getByLabelText(/Source/) as HTMLInputElement;
        expect(sourceField.value).toBe(recipe.source);

        const sourceUrlField = screen.getByLabelText(/Web Address/) as HTMLInputElement;
        expect(sourceUrlField.value).toBe(recipe.sourceUrl);

        const timeField = screen.getByLabelText(/^Time/) as HTMLInputElement;
        expect(timeField.value).toBe(recipe.time?.toString());

        const activeTimeField = screen.getByLabelText(/Active Time/) as HTMLInputElement;
        expect(activeTimeField.value).toBe(recipe.activeTime?.toString());

        const caloriesField = screen.getByLabelText(/Calories/) as HTMLInputElement;
        expect(caloriesField.value).toBe(recipe.calories?.toString());

        const proteinField = screen.getByLabelText(/Protein/) as HTMLInputElement;
        expect(proteinField.value).toBe(recipe.protein?.toString());

        const carbsField = screen.getByLabelText(/Carbohydrates/) as HTMLInputElement;
        expect(carbsField.value).toBe(recipe.carbohydrates?.toString());

        const fatField = screen.getByLabelText(/Fat/) as HTMLInputElement;
        expect(fatField.value).toBe(recipe.fat?.toString());

        const sugarField = screen.getByLabelText(/Sugar/) as HTMLInputElement;
        expect(sugarField.value).toBe(recipe.sugar?.toString());

        const cholesterolField = screen.getByLabelText(/Cholesterol/) as HTMLInputElement;
        expect(cholesterolField.value).toBe(recipe.cholesterol?.toString());

        const FiberField = screen.getByLabelText(/Fiber/) as HTMLInputElement;
        expect(FiberField.value).toBe(recipe.fiber?.toString());

        expect(screen.queryAllByTestId(/^ingredient-/).length).toBe(10);
        expect(screen.queryAllByTestId(/^direction-/).length).toBe(5);
    });
});

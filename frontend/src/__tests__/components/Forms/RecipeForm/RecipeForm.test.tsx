import {
    act,
    screen,
} from '@testing-library/react';
import { v4 as uuid4 } from 'uuid';
import { renderWithRouter } from '@test/renderWithRouter';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import RecipeForm from '@components/Forms/RecipeForm';
import userEvent from '@testing-library/user-event';
import Recipe from '@models/Recipe';
import { MockIngredientList } from '@test/mocks/MockIngredient';
import { MockStepList } from '@test/mocks/MockStep';

describe('<RecipeForm />', () => {
    test('It loads the form with the default display', () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm onSave={jest.fn()} />
            </MockAppProvider>,
        );

        const ingredients = screen.getAllByTestId(/^ingredient-input-/);
        const directions = screen.getAllByTestId(/^direction-input-/);

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

        const ingredients = screen.getAllByTestId(/^ingredient-input-/);

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

        expect(screen.queryByTestId(/^ingredient-input-1/)).toBeInTheDocument();
        expect(screen.queryByTestId(/^ingredient-input-2/)).not.toBeInTheDocument();
        expect(screen.queryByTestId(/^ingredient-input-3/)).toBeInTheDocument();
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

        expect(screen.queryByTestId(/^direction-input-1/)).toBeInTheDocument();
        expect(screen.queryByTestId(/^direction-input-2/)).not.toBeInTheDocument();
        expect(screen.queryByTestId(/^direction-input-3/)).toBeInTheDocument();
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

        expect(screen.queryAllByTestId(/^ingredient-input-/).length).toBe(2);
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

        expect(screen.queryAllByTestId(/^direction-input/).length).toBe(2);
        expect(screen.queryAllByTestId(/^delete-step-/).length).toBe(2);
    });

    test('It loads an existing recipe', async () => {
        const userId = uuid4();

        const recipe: Recipe = {
            recipeId: 1,
            id: uuid4(),
            name: 'Mock Recipe',
            imageData: '',
            imageUrl: 'http://localhost:3000/image.jpg',
            largeImageData: '',
            imageUrlLarge: 'http://localhost:3000/image_lg.jpg',
            isPublic: true,
            servings: 4,
            activeTime: 45,
            time: 60,
            calories: 400,
            carbohydrates: 25,
            fat: 8,
            protein: 22,
            cholesterol: 100,
            fiber: 12,
            sugar: 9,
            description: '<p>This is a test recipe</p>',
            source: 'Google',
            sourceUrl: 'https://www.google.com',
            userAccount: {
                id: userId,
                userId,
                roles: [],
                name: 'Test User',
                email: 'testuser@gmail.com',
            },
            userAccountId: userId,
            ingredients: MockIngredientList(1, 10),
            steps: MockStepList(1, 5),
            categories: [],
            meats: [],
            notes: [],
            isFavorite: false,
            dateCreated: new Date(),
            dateUpdated: new Date(),
        };

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipeForm
                    recipe={recipe}
                    onSave={jest.fn()}
                />
            </MockAppProvider>,
        );

        const nameField = screen.getByLabelText(/Name/) as HTMLInputElement;
        expect(nameField.value).toBe('Mock Recipe');

        const servingsField = screen.getByLabelText(/Servings/) as HTMLInputElement;
        expect(servingsField.value).toBe('4');

        const sourceField = screen.getByLabelText(/Source/) as HTMLInputElement;
        expect(sourceField.value).toBe('Google');

        const sourceUrlField = screen.getByLabelText(/Web Address/) as HTMLInputElement;
        expect(sourceUrlField.value).toBe('https://www.google.com');

        const timeField = screen.getByLabelText(/^Time/) as HTMLInputElement;
        expect(timeField.value).toBe('60');

        const activeTimeField = screen.getByLabelText(/Active Time/) as HTMLInputElement;
        expect(activeTimeField.value).toBe('45');

        const caloriesField = screen.getByLabelText(/Calories/) as HTMLInputElement;
        expect(caloriesField.value).toBe('400');

        const proteinField = screen.getByLabelText(/Protein/) as HTMLInputElement;
        expect(proteinField.value).toBe('22');

        const carbsField = screen.getByLabelText(/Carbohydrates/) as HTMLInputElement;
        expect(carbsField.value).toBe('25');

        const fatField = screen.getByLabelText(/Fat/) as HTMLInputElement;
        expect(fatField.value).toBe('8');

        const sugarField = screen.getByLabelText(/Sugar/) as HTMLInputElement;
        expect(sugarField.value).toBe('9');

        const cholesterolField = screen.getByLabelText(/Cholesterol/) as HTMLInputElement;
        expect(cholesterolField.value).toBe('100');

        const FiberField = screen.getByLabelText(/Fiber/) as HTMLInputElement;
        expect(FiberField.value).toBe('12');

        expect(screen.queryAllByTestId(/^ingredient-input-/).length).toBe(10);
        expect(screen.queryAllByTestId(/^direction-input-/).length).toBe(5);
    });
});

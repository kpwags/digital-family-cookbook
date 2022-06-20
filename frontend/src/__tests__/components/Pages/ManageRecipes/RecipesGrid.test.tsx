import { screen } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import RecipesGrid from '@components/Pages/ManageRecipes/RecipesGrid';
import { MockRecipeList } from '@test/mocks/MockRecipe';
import { renderWithRouter } from '@test/renderWithRouter';

describe('<RecipesGrid />', () => {
    test('It renders the recipes in a grid', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <RecipesGrid
                    loadingMessage=""
                    recipes={MockRecipeList(10)}
                    onEditRecipe={jest.fn()}
                    onDeleteRecipe={jest.fn()}
                />
            </MockAppProvider>,
        );

        expect(screen.queryAllByRole('button', { name: 'Edit' })).toHaveLength(10);
        expect(screen.queryAllByRole('button', { name: 'Delete' })).toHaveLength(10);
    });
});

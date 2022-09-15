import { screen, waitForElementToBeRemoved } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import MostFavoritedRecipes from '@components/MostFavoritedRecipes';

describe('<MostFavoritedRecipes />', () => {
    test('It displays the most favorited recipes', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MostFavoritedRecipes count={8} />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText(/Loading.../));

        const recipeCards = screen.queryAllByTestId(/recipe-card-/i);

        expect(recipeCards).toHaveLength(8);
    });
});

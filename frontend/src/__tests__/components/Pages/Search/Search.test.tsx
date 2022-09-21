import { screen, waitForElementToBeRemoved } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import Search from '@components/Pages/Search';
import userEvent from '@testing-library/user-event';

describe('<Search />', () => {
    test('It loads the search page without any results', () => {
        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <Search />
            </MockAppProvider>,
            {
                route: '/search',
            },
        );

        expect(screen.queryByTestId(/search-keywords/)).toBeInTheDocument();
        expect(screen.queryByText(/Search Results for/)).not.toBeInTheDocument();
    });

    test('It pre-loads the search', async () => {
        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <Search />
            </MockAppProvider>,
            {
                route: '/search?q=test&p=1&rpp=10',
            },
        );

        await waitForElementToBeRemoved(screen.queryByText(/Loading.../));

        expect(screen.queryByText(/Search Results for/)).toBeInTheDocument();

        const keywordsInput = screen.getByTestId(/search-keywords/) as HTMLInputElement;

        expect(keywordsInput.value).toBe('test');
    });

    test('It alerts the user there are no results for a search', async () => {
        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <Search />
            </MockAppProvider>,
            {
                route: '/search',
            },
        );

        const keywordsInput = screen.getByTestId(/search-keywords/) as HTMLInputElement;

        userEvent.type(keywordsInput, 'noresults');

        userEvent.click(screen.getByRole('button', { name: 'Search' }));

        await waitForElementToBeRemoved(screen.queryByText(/Loading.../));

        expect(screen.queryByText(/No recipes found for 'noresults'/)).toBeInTheDocument();
    });

    test('It returns a list of recipes that match the search terms', async () => {
        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <Search />
            </MockAppProvider>,
            {
                route: '/search',
            },
        );

        const keywordsInput = screen.getByTestId(/search-keywords/) as HTMLInputElement;

        userEvent.type(keywordsInput, 'chicken');

        userEvent.click(screen.getByRole('button', { name: 'Search' }));

        await waitForElementToBeRemoved(screen.queryByText(/Loading.../));

        expect(screen.queryByText(/Search Results for 'chicken'/)).toBeInTheDocument();
        expect(screen.queryByText(/No recipes found for 'chicken'/)).not.toBeInTheDocument();
        expect(screen.queryAllByTestId(/recipe-card-/)).toHaveLength(8);
    });
});

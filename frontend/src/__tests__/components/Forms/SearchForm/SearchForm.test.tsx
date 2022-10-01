import { screen } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import SearchForm from '@components/Forms/SearchForm';
import userEvent from '@testing-library/user-event';

describe('<SearchForm />', () => {
    test('It searches when the user enters keywords', async () => {
        const mockSearch = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <SearchForm
                    onSearch={mockSearch}
                    keywords=""
                    processingMessage=""
                />
            </MockAppProvider>,
        );

        const keywordsInput = screen.getByTestId(/search-keywords/) as HTMLInputElement;

        userEvent.type(keywordsInput, 'search');

        userEvent.click(screen.getByRole('button', { name: 'Search' }));

        expect(mockSearch).toHaveBeenCalledTimes(1);
    });

    test('It errors if no keywords are entered', async () => {
        const mockSearch = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <SearchForm
                    onSearch={mockSearch}
                    keywords=""
                    processingMessage=""
                />
            </MockAppProvider>,
        );

        const keywordsInput = screen.getByTestId(/search-keywords/) as HTMLInputElement;

        userEvent.clear(keywordsInput);

        userEvent.click(screen.getByRole('button', { name: 'Search' }));

        expect(screen.queryByText(/Please enter search keywords/)).toBeInTheDocument();
        expect(mockSearch).toHaveBeenCalledTimes(0);
    });
});

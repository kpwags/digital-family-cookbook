import { screen, waitForElementToBeRemoved } from '@testing-library/react';
import Router from 'react-router';
import RecipesByMeat from '@components/Pages/RecipesByMeat';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';

jest.mock('react-router', () => ({
    ...jest.requireActual('react-router'),
    useParams: jest.fn(),
}));

describe('<RecipesByMeat />', () => {
    test('It displays a grid of recipes', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipesByMeat />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryAllByTestId(/recipe-card-/)).toHaveLength(8);
    });

    test('It tells the user there are no recipes', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '100' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipesByMeat />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        await screen.findByText(/No recipes were found with the meat 'Mock Meat'/);
    });
});

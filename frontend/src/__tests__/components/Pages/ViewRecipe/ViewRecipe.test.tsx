import { screen, waitForElementToBeRemoved } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockUserAccount } from '@test/mocks/MockUsers';
import ViewRecipe from '@components/Pages/ViewRecipe';
import { renderWithRouter } from '@test/renderWithRouter';
import Router from 'react-router';

jest.mock('react-router', () => ({
    ...jest.requireActual('react-router'),
    useParams: jest.fn(),
}));

describe('<ViewRecipe />', () => {
    test('It renders the recipe', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <ViewRecipe />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryByText(/Test Recipe 01/)).toBeInTheDocument();
        expect(screen.queryByText(/Description/)).toBeInTheDocument();
        expect(screen.queryByText(/Nutrition Info/)).toBeInTheDocument();
        expect(screen.queryByTestId(/recipe-image/)).not.toBeInTheDocument();
    });

    test('It does not display a blank description', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '2' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <ViewRecipe />
            </MockAppProvider>,
            { route: '/recipes/view/2' },
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryByText(/Description/)).not.toBeInTheDocument();
    });

    test('It does not display nutrition info for a recipe without nutrition', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '3' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <ViewRecipe />
            </MockAppProvider>,
            { route: '/recipes/view/3' },
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryByText(/Nutrition Info/)).not.toBeInTheDocument();
    });

    test('It will show the image if the recipe has an image', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '4' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <ViewRecipe />
            </MockAppProvider>,
            { route: '/recipes/view/4' },
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryByTestId(/recipe-image/)).toBeInTheDocument();
    });

    test('It alerts the user that the recipe cannot be found', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '999' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <ViewRecipe />
            </MockAppProvider>,
            { route: '/recipes/view/999' },
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryByText(/Unable to find recipe/)).toBeInTheDocument();
    });
});

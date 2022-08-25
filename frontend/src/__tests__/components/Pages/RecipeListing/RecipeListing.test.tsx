import { screen, waitForElementToBeRemoved } from '@testing-library/react';
import Router from 'react-router';
import RecipeListing from '@components/Pages/RecipeListing';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';

jest.mock('react-router', () => ({
    ...jest.requireActual('react-router'),
    useParams: jest.fn(),
}));

describe('<RecipeListing />', () => {
    test('It displays a grid of recipes for catgories', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '1', page: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipeListing mode="category" />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryAllByTestId(/recipe-card-/)).toHaveLength(8);
    });

    test('It tells the user there are no recipes for the specified category', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '100', page: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipeListing mode="category" />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        await screen.findByText(/No recipes were found with the category 'Mock Category'/);
    });

    test('It displays a grid of recipes for meats', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '1', page: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipeListing mode="meat" />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryAllByTestId(/recipe-card-/)).toHaveLength(8);
    });

    test('It tells the user there are no recipes for the specified meat', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '100', page: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipeListing mode="meat" />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        await screen.findByText(/No recipes were found with the meat 'Mock Meat'/);
    });

    test('It displays a grid of recipes for users', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '123456', page: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipeListing mode="user" />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        expect(screen.queryAllByTestId(/recipe-card-/)).toHaveLength(8);
    });

    test('It tells the user there are no recipes for the specified user', async () => {
        jest.spyOn(Router, 'useParams').mockReturnValue({ id: '100', page: '1' });

        renderWithRouter(
            <MockAppProvider user={MockUserAccount}>
                <RecipeListing mode="user" />
            </MockAppProvider>,
        );

        await waitForElementToBeRemoved(() => screen.queryByText('Loading...'));

        await screen.findByText(/No recipes were found created by Mock User/);
    });
});

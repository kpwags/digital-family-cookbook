import { screen } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { renderWithRouter } from '@test/renderWithRouter';
import Landing from '@components/Pages/Landing';

describe('<Landing />', () => {
    test('It shows the public landing page', () => {
        renderWithRouter(
            <MockAppProvider
                siteSettings={{
                    isPublic: true,
                    allowPublicRegistration: false,
                    id: '1',
                    landingPageText: 'This is a site to manage recipes.',
                    invitationCode: '',
                    siteSettingsId: 1,
                    title: 'Digital Family Cookbook',
                }}
            >
                <Landing />
            </MockAppProvider>,
        );

        expect(screen.queryByText('Welcome to Digital Family Cookbook')).toBeInTheDocument();
        expect(screen.queryByText('This is a site to manage recipes.')).toBeInTheDocument();
        expect(screen.queryByText('Most Recent Recipes')).toBeInTheDocument();
        expect(screen.queryByText('Most Favorited Recipes')).toBeInTheDocument();
    });

    test('It shows the private landing page', () => {
        renderWithRouter(
            <MockAppProvider
                siteSettings={{
                    isPublic: false,
                    allowPublicRegistration: false,
                    id: '1',
                    landingPageText: 'This is a site to manage recipes.',
                    invitationCode: '',
                    siteSettingsId: 1,
                    title: 'Digital Family Cookbook',
                }}
            >
                <Landing />
            </MockAppProvider>,
        );

        expect(screen.queryByText('Welcome to Digital Family Cookbook')).toBeInTheDocument();
        expect(screen.queryByText('This is a site to manage recipes.')).toBeInTheDocument();
        expect(screen.queryByText('Most Recent Recipes')).not.toBeInTheDocument();
        expect(screen.queryByText('Most Favorited Recipes')).not.toBeInTheDocument();
    });
});

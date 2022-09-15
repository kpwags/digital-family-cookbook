import { screen } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import Landing from '@components/Pages/Landing';

describe('<Landing />', () => {
    test('It shows the public landing page', () => {
        renderWithRouter(
            <MockAppProvider
                user={MockAdminUserAccount}
                siteSettings={{
                    isPublic: true,
                    allowPublicRegistration: false,
                    id: '1',
                    invitationCode: '',
                    siteSettingsId: 1,
                    title: 'Digital Family Cookbook',
                }}
            >
                <Landing />
            </MockAppProvider>,
        );

        expect(screen.queryByText('Most Recent Recipes')).toBeInTheDocument();
        expect(screen.queryByText('Most Favorited Recipes')).toBeInTheDocument();
    });

    test('It shows the private landing page', () => {
        renderWithRouter(
            <MockAppProvider
                user={MockAdminUserAccount}
                siteSettings={{
                    isPublic: false,
                    allowPublicRegistration: false,
                    id: '1',
                    invitationCode: '',
                    siteSettingsId: 1,
                    title: 'Digital Family Cookbook',
                }}
            >
                <Landing />
            </MockAppProvider>,
        );

        expect(screen.queryByText('Site is not public!')).toBeInTheDocument();
    });
});

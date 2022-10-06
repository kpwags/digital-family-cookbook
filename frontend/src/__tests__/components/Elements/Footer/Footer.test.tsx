import { screen } from '@testing-library/react';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import Footer from '@components/Elements/Footer';
import copyObject from '@utils/copyObject';
import { defaultSiteSettings } from '@utils/defaults';

describe('<Footer />', () => {
    test('it hides the links when a user is not logged in', () => {
        renderWithRouter(
            <MockAppProvider>
                <Footer />
            </MockAppProvider>,
        );

        expect(screen.queryByText(/Home/)).toBeInTheDocument();
        expect(screen.queryByText(/Search/)).toBeInTheDocument();
        expect(screen.queryByText(/Add Recipe/)).not.toBeInTheDocument();
    });

    test('it hides the links when a user is not logged in and the site is private', () => {
        const mockSiteSettings = copyObject(defaultSiteSettings);
        mockSiteSettings.invitationCode = 'dfa26202-1f0a-4be6-a326-9f675cd992bf';
        mockSiteSettings.isPublic = false;

        renderWithRouter(
            <MockAppProvider
                siteSettings={mockSiteSettings}
            >
                <Footer />
            </MockAppProvider>,
        );

        expect(screen.queryByText(/Home/)).toBeInTheDocument();
        expect(screen.queryByText(/Search/)).not.toBeInTheDocument();
        expect(screen.queryByText(/Add Recipe/)).not.toBeInTheDocument();
    });

    test('it shows the appropriate links when the user is logged in', () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <Footer />
            </MockAppProvider>,
        );

        expect(screen.queryByText(/Home/)).toBeInTheDocument();
        expect(screen.queryByText(/Search/)).toBeInTheDocument();
        expect(screen.queryByText(/Add Recipe/)).toBeInTheDocument();
    });
});

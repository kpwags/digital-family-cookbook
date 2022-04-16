import { screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import ManageCategories from '@components/Pages/ManageCategories';

describe('<ManageCategories />', () => {
    test('It opens the form when the add button is clicked', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <ManageCategories />
            </MockAppProvider>,
        );

        const addButton = await screen.findByRole('button', { name: /Add Category/ });

        userEvent.click(addButton);

        expect(screen.queryByTestId(/category-form-modal/)).toBeVisible();
    });
});

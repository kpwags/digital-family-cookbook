import { screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import ManageMeats from '@components/Pages/ManageMeats';

describe('<ManageMeats />', () => {
    test('It opens the form when the add button is clicked', () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <ManageMeats />
            </MockAppProvider>,
        );

        const addButton = screen.getByRole('button', { name: /Add Meat/ });

        userEvent.click(addButton);

        expect(screen.queryByTestId(/meat-form-modal/)).toBeVisible();
    });
});

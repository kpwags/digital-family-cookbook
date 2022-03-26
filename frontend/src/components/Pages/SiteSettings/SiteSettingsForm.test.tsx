import { screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import SiteSettingsForm from './SiteSettingsForm';

describe('<SiteSettingsForm />', () => {
    test('It renders the form', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <SiteSettingsForm />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Title/) as HTMLInputElement;
        const isPublicCheckbox = await screen.findByLabelText(/Is Public/) as HTMLInputElement;
        const allowPublicRegistrationCheckbox = await screen.findByLabelText(/Allow Public Registration/) as HTMLInputElement;

        expect(nameField.value).toBe('Digital Family Cookbook');
        expect(isPublicCheckbox.checked).toBeTruthy();
        expect(allowPublicRegistrationCheckbox.checked).toBeFalsy();

        await screen.findByText(/dfa26202-1f0a-4be6-a326-9f675cd992bf/);
    });

    test('It validates the form', async () => {
        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <SiteSettingsForm />
            </MockAppProvider>,
        );

        const nameField = await screen.findByLabelText(/Title/) as HTMLInputElement;

        userEvent.clear(nameField);

        const saveButton = await screen.findByRole('button', { name: 'Save' });

        await userEvent.click(saveButton);

        await screen.findByText(/Title is required/);
    });
});

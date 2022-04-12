import {
    fireEvent,
    waitForElementToBeRemoved,
    render,
    screen,
    act,
} from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { defaultSiteSettings } from '@utils/defaults';
import copyObject from '@utils/copyObject';
import RegisterForm from '.';

describe('<RegisterForm />', () => {
    test('It renders the registration form', async () => {
        render(
            <MockAppProvider>
                <RegisterForm onRegisterCompleted={jest.fn()} />
            </MockAppProvider>,
        );

        await screen.findByLabelText(/Name/);
        await screen.findByLabelText(/Email/);
        await screen.findByLabelText('Password');
        await screen.findByLabelText(/Re-Enter Password/);
        await screen.findByLabelText(/Invitation Code/);
        await screen.findByRole('button', { name: 'Register' });
    });

    test('It renders the registration form when public registration is allowed', async () => {
        const siteSettings = copyObject(defaultSiteSettings);
        siteSettings.allowPublicRegistration = true;

        render(
            <MockAppProvider siteSettings={siteSettings}>
                <RegisterForm onRegisterCompleted={jest.fn()} />
            </MockAppProvider>,
        );

        await screen.findByLabelText(/Name/);
        await screen.findByLabelText(/Email/);
        await screen.findByLabelText('Password');
        await screen.findByLabelText(/Re-Enter Password/);
        await screen.findByRole('button', { name: 'Register' });

        expect(screen.queryByLabelText(/Invitation Code/)).not.toBeInTheDocument();
    });

    test('It validates the registration form', async () => {
        render(
            <MockAppProvider>
                <RegisterForm onRegisterCompleted={jest.fn()} />
            </MockAppProvider>,
        );

        const loginButton = await screen.findByRole('button', { name: 'Register' });

        userEvent.click(loginButton);

        await screen.findByText(/Name is required/);
        await screen.findByText(/Email is required/);
        await screen.findByText('Password is required');
        await screen.findByText(/Confirm Password is required/);

        const passwordField1: HTMLInputElement = await screen.findByLabelText('Password');
        const passwordField2: HTMLInputElement = await screen.findByLabelText(/Re-Enter Password/);

        await act(async () => {
            userEvent.clear(passwordField1);
            userEvent.type(passwordField1, 'password1');

            await fireEvent.blur(passwordField1);

            userEvent.clear(passwordField2);
            userEvent.type(passwordField2, 'password2');

            await fireEvent.blur(passwordField2);

            await userEvent.click(loginButton);
        });

        const passwordMismatch = await screen.findAllByText(/The passwords that you entered do not match/);

        expect(passwordMismatch.length).toBe(2);
    });

    test('It successfully registers the user', async () => {
        const mockRegisterCompleted = jest.fn();

        render(
            <MockAppProvider>
                <RegisterForm onRegisterCompleted={() => { mockRegisterCompleted(); }} />
            </MockAppProvider>,
        );

        const nameField: HTMLInputElement = await screen.findByLabelText(/Name/);
        const emailField: HTMLInputElement = await screen.findByLabelText(/Email/);
        const passwordField1: HTMLInputElement = await screen.findByLabelText('Password');
        const passwordField2: HTMLInputElement = await screen.findByLabelText(/Re-Enter Password/);
        const invitationCodeField: HTMLInputElement = await screen.findByLabelText(/Invitation Code/);
        const registerButton = await screen.findByRole('button', { name: 'Register' });

        await act(async () => {
            await userEvent.clear(nameField);
            await userEvent.clear(emailField);
            await userEvent.clear(passwordField1);
            await userEvent.clear(passwordField2);
            await userEvent.clear(invitationCodeField);

            await userEvent.type(nameField, 'Test');
            await userEvent.type(emailField, 'test@testing.com');
            await userEvent.type(passwordField1, 'validPassword123');
            await userEvent.type(passwordField2, 'validPassword123');
            await userEvent.type(invitationCodeField, 'please');

            await userEvent.click(registerButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Registerring.../));

        expect(mockRegisterCompleted).toHaveBeenCalledTimes(1);
    });

    test('It errors registering the user with an invalid invitation code', async () => {
        const mockRegisterCompleted = jest.fn();

        render(
            <MockAppProvider>
                <RegisterForm onRegisterCompleted={() => { mockRegisterCompleted(); }} />
            </MockAppProvider>,
        );

        const nameField: HTMLInputElement = await screen.findByLabelText(/Name/);
        const emailField: HTMLInputElement = await screen.findByLabelText(/Email/);
        const passwordField1: HTMLInputElement = await screen.findByLabelText('Password');
        const passwordField2: HTMLInputElement = await screen.findByLabelText(/Re-Enter Password/);
        const invitationCodeField: HTMLInputElement = await screen.findByLabelText(/Invitation Code/);
        const registerButton = await screen.findByRole('button', { name: 'Register' });

        await act(async () => {
            await userEvent.clear(nameField);
            await userEvent.clear(emailField);
            await userEvent.clear(passwordField1);
            await userEvent.clear(passwordField2);
            await userEvent.clear(invitationCodeField);

            await userEvent.type(nameField, 'Test');
            await userEvent.type(emailField, 'test@testing.com');
            await userEvent.type(passwordField1, 'validPassword123');
            await userEvent.type(passwordField2, 'validPassword123');
            await userEvent.type(invitationCodeField, 'wrong');

            await userEvent.click(registerButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Registerring.../));

        await screen.findByText(/Invalid invitation code/);
    });
});

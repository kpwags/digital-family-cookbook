import {
    waitForElementToBeRemoved,
    render,
    screen,
    act,
} from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import LoginForm from '.';

describe('<LoginForm />', () => {
    test('It renders the login form', async () => {
        render(
            <MockAppProvider>
                <LoginForm onLoginCompleted={jest.fn()} />
            </MockAppProvider>,
        );

        await screen.findByLabelText(/Email/);
        await screen.findByLabelText(/Password/);
        await screen.findByRole('button', { name: 'Login' });
    });

    test('It validates the login form', async () => {
        render(
            <MockAppProvider>
                <LoginForm onLoginCompleted={jest.fn()} />
            </MockAppProvider>,
        );

        const loginButton = await screen.findByRole('button', { name: 'Login' });

        userEvent.click(loginButton);

        await screen.findByText(/Email is required/);
        await screen.findByText(/Password is required/);
    });

    test('It displays an error when the user types an invalid email & password', async () => {
        render(
            <MockAppProvider>
                <LoginForm onLoginCompleted={jest.fn()} />
            </MockAppProvider>,
        );

        const emailField: HTMLInputElement = await screen.findByLabelText(/Email/);
        const passwordField: HTMLInputElement = await screen.findByLabelText(/Password/);
        const loginButton = await screen.findByRole('button', { name: 'Login' });

        userEvent.type(emailField, 'blah@bademail.com');
        userEvent.type(passwordField, 'iamhacker');
        userEvent.click(loginButton);

        await screen.findByText(/Unable to verify username or password/);
    });

    test('It successfully logs the user in', async () => {
        const mockLoginCompleted = jest.fn();

        render(
            <MockAppProvider>
                <LoginForm onLoginCompleted={() => { mockLoginCompleted(); }} />
            </MockAppProvider>,
        );

        const emailField: HTMLInputElement = await screen.findByLabelText(/Email/);
        const passwordField: HTMLInputElement = await screen.findByLabelText(/Password/);
        const loginButton = await screen.findByRole('button', { name: 'Login' });

        await act(async () => {
            await userEvent.clear(emailField);
            await userEvent.clear(passwordField);

            await userEvent.type(emailField, 'test@testing.com');
            await userEvent.type(passwordField, 'validPassword123');
            await userEvent.click(loginButton);
        });

        await waitForElementToBeRemoved(() => screen.queryByText(/Logging In.../));

        expect(screen.queryByText(/Unable to verify username or password/)).not.toBeInTheDocument();
        expect(mockLoginCompleted).toHaveBeenCalledTimes(1);
    });
});

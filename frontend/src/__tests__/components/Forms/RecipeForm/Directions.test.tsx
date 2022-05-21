import {
    render,
    screen,
} from '@testing-library/react';
import { Form } from 'antd';
import { MockAppProvider } from '@test/MockAppProvider';
import Directions from '@components/Forms/RecipeForm/Directions';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import userEvent from '@testing-library/user-event';

describe('<Directions />', () => {
    test('it does not allow the user to delete the only step', () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <Form>
                    <Directions
                        directions={[{
                            id: 1,
                            name: '',
                            sortOrder: 1,
                        }]}
                        onDragEnd={jest.fn()}
                        onDirectionUpdate={jest.fn()}
                        onDirectionRemove={jest.fn()}
                    />
                </Form>
            </MockAppProvider>,
        );

        expect(screen.queryByTestId(/delete-step-/)).not.toBeVisible();
    });

    test('it allows for any step to be deleted', async () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <Form>
                    <Directions
                        directions={[
                            {
                                id: 1,
                                name: 'Step 1',
                                sortOrder: 1,
                            },
                            {
                                id: 2,
                                name: 'Step 2',
                                sortOrder: 2,
                            }]}
                        onDragEnd={jest.fn()}
                        onDirectionUpdate={jest.fn()}
                        onDirectionRemove={jest.fn()}
                    />
                </Form>
            </MockAppProvider>,
        );

        const deleteButtons = await screen.findAllByTestId(/delete-step-/);

        expect(deleteButtons.length).toBe(2);
    });

    test('it deletes a step', async () => {
        const mockDelete = jest.fn();

        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <Form>
                    <Directions
                        directions={[
                            {
                                id: 1,
                                name: 'Step 1',
                                sortOrder: 1,
                            },
                            {
                                id: 2,
                                name: 'Step 2',
                                sortOrder: 2,
                            }]}
                        onDragEnd={jest.fn()}
                        onDirectionUpdate={jest.fn()}
                        onDirectionRemove={mockDelete}
                    />
                </Form>
            </MockAppProvider>,
        );

        const deleteButtons = await screen.findAllByTestId(/delete-step-/);

        userEvent.click(deleteButtons[0]);

        expect(mockDelete).toBeCalledTimes(1);
    });
});

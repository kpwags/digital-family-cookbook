import {
    render,
    screen,
} from '@testing-library/react';
import { Form } from 'antd';
import { MockAppProvider } from '@test/MockAppProvider';
import Ingredients from '@components/Forms/RecipeForm/Ingredients';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import userEvent from '@testing-library/user-event';

describe('<Ingredients />', () => {
    test('it does not allow the user to delete the only ingredient', () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <Form>
                    <Ingredients
                        ingredients={[{
                            id: 1,
                            name: '',
                            sortOrder: 1,
                        }]}
                        onDragEnd={jest.fn()}
                        onIngredientUpdate={jest.fn()}
                        onIngredientRemove={jest.fn()}
                    />
                </Form>
            </MockAppProvider>,
        );

        expect(screen.queryByTestId(/delete-ingredient-/)).not.toBeVisible();
    });

    test('it allows for any ingredient to be deleted', async () => {
        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <Form>
                    <Ingredients
                        ingredients={[
                            {
                                id: 1,
                                name: 'Ingredient 1',
                                sortOrder: 1,
                            },
                            {
                                id: 2,
                                name: 'Ingredient 2',
                                sortOrder: 2,
                            }]}
                        onDragEnd={jest.fn()}
                        onIngredientUpdate={jest.fn()}
                        onIngredientRemove={jest.fn()}
                    />
                </Form>
            </MockAppProvider>,
        );

        const deleteButtons = await screen.findAllByTestId(/delete-ingredient-/);

        expect(deleteButtons.length).toBe(2);
    });

    test('it deletes an ingredient', async () => {
        const mockDelete = jest.fn();

        render(
            <MockAppProvider user={MockAdminUserAccount}>
                <Form>
                    <Ingredients
                        ingredients={[
                            {
                                id: 1,
                                name: 'Ingredient 1',
                                sortOrder: 1,
                            },
                            {
                                id: 2,
                                name: 'Ingredient 2',
                                sortOrder: 2,
                            }]}
                        onDragEnd={jest.fn()}
                        onIngredientUpdate={jest.fn()}
                        onIngredientRemove={mockDelete}
                    />
                </Form>
            </MockAppProvider>,
        );

        const deleteButtons = await screen.findAllByTestId(/delete-ingredient-/);

        userEvent.click(deleteButtons[0]);

        expect(mockDelete).toBeCalledTimes(1);
    });
});

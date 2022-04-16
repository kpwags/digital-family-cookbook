import { screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import CategoriesGrid from '@components/Pages/ManageCategories/CategoriesGrid';
import { MockCategory, MockCategoryList } from '@test/mocks/MockCategory';

describe('<CategoriesGrid />', () => {
    test('It renders the grid', async () => {
        const categories = MockCategoryList();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoriesGrid
                    categories={categories}
                    loadingMessage=""
                    onEditCategory={jest.fn()}
                    onCategoryChanged={jest.fn()}
                />
            </MockAppProvider>,
        );

        for (let i = 0; i < categories.length; i += 1) {
            expect(screen.queryByText(categories[i].name)).not.toBeNull();
        }
    });

    test('It opens the edit form when pressing edit', async () => {
        const mockOnEditCategory = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoriesGrid
                    categories={[MockCategory()]}
                    loadingMessage=""
                    onEditCategory={mockOnEditCategory}
                    onCategoryChanged={jest.fn()}
                />
            </MockAppProvider>,
        );

        const editButton = await screen.findByText(/Edit/);

        userEvent.click(editButton);

        expect(mockOnEditCategory).toBeCalledTimes(1);
    });

    test('It opens the delete form when pressing edit', async () => {
        const category = MockCategory();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <CategoriesGrid
                    categories={[category]}
                    loadingMessage=""
                    onEditCategory={jest.fn()}
                    onCategoryChanged={jest.fn()}
                />
            </MockAppProvider>,
        );

        const deleteButton = await screen.findByText(/Delete/);

        userEvent.click(deleteButton);

        await screen.findByText(`Are you sure you want to delete ${category.name}?`);
    });
});

import { screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MockAppProvider } from '@test/MockAppProvider';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { renderWithRouter } from '@test/renderWithRouter';
import MeatsGrid from '@components/Pages/ManageMeats/MeatsGrid';
import { MockMeat, MockMeatList } from '@test/mocks/MockMeat';

describe('<MeatsGrid />', () => {
    test('It renders the grid', () => {
        const meats = MockMeatList();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatsGrid
                    meats={meats}
                    loadingMessage=""
                    onEditMeat={jest.fn()}
                    onMeatChanged={jest.fn()}
                />
            </MockAppProvider>,
        );

        for (let i = 0; i < meats.length; i += 1) {
            expect(screen.queryByText(meats[i].name)).not.toBeNull();
        }
    });

    test('It opens the edit form when pressing edit', () => {
        const mockOnEditCategory = jest.fn();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatsGrid
                    meats={[MockMeat()]}
                    loadingMessage=""
                    onEditMeat={mockOnEditCategory}
                    onMeatChanged={jest.fn()}
                />
            </MockAppProvider>,
        );

        const editButton = screen.getByText(/Edit/);

        userEvent.click(editButton);

        expect(mockOnEditCategory).toBeCalledTimes(1);
    });

    test('It opens the delete form when pressing edit', () => {
        const meat = MockMeat();

        renderWithRouter(
            <MockAppProvider user={MockAdminUserAccount}>
                <MeatsGrid
                    meats={[meat]}
                    loadingMessage=""
                    onEditMeat={jest.fn()}
                    onMeatChanged={jest.fn()}
                />
            </MockAppProvider>,
        );

        const deleteButton = screen.getByText(/Delete/);

        userEvent.click(deleteButton);

        expect(screen.queryByText(`Are you sure you want to delete ${meat.name}?`)).toBeInTheDocument();
    });
});

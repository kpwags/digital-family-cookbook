import DataGenerator from '@test/DataGenerator';
import { Category } from '@models/Category';

export const MockCategory = (id = 0): Category => ({
    id: DataGenerator.GenerateGuid(),
    categoryId: id === 0 ? DataGenerator.GenerateInteger(1, 250) : id,
    name: DataGenerator.GenerateString(),
});

export const MockCategoryList = (length = 10): Category[] => {
    const categories: Category[] = [];

    for (let i = 0; i < length; i += 1) {
        categories.push(MockCategory(i + 1));
    }

    return categories;
};

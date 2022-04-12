import DataGenerator from '@test/DataGenerator';
import { Category } from '@models/Category';

export const MockCategory = (): Category => ({
    id: DataGenerator.GenerateGuid(),
    categoryId: DataGenerator.GenerateInteger(1, 250),
    name: DataGenerator.GenerateString(),
});

export const MockCategoryList = (length = 10): Category[] => {
    const categories: Category[] = [];

    for (let i = 0; i < length; i += 1) {
        categories.push(MockCategory());
    }

    return categories;
};

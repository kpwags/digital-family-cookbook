import DataGenerator from '@test/DataGenerator';
import Recipe from '@models/Recipe';
import { MockMeatList } from './MockMeat';
import { MockCategoryList } from './MockCategory';
import { MockIngredientList } from './MockIngredient';
import { MockStepList } from './MockStep';
import { MockUserAccount } from './MockUsers';

export const MockRecipe = (): Recipe => {
    const recipeId = DataGenerator.GenerateInteger(1, 100);

    return {
        recipeId,
        id: DataGenerator.GenerateGuid(),
        name: DataGenerator.GenerateString(15),
        description: DataGenerator.GenerateString(100),
        servings: DataGenerator.GenerateInteger(1, 10),
        time: DataGenerator.GenerateInteger(10, 60),
        activeTime: DataGenerator.GenerateInteger(5, 55),
        isPublic: DataGenerator.GenerateInteger(1, 100) % 2 === 0,
        imageUrl: '',
        imageUrlLarge: '',
        imageData: '',
        largeImageData: '',
        calories: DataGenerator.GenerateInteger(0, 400),
        protein: DataGenerator.GenerateInteger(0, 40),
        carbohydrates: DataGenerator.GenerateInteger(0, 40),
        fat: DataGenerator.GenerateInteger(0, 40),
        sugar: DataGenerator.GenerateInteger(0, 40),
        fiber: DataGenerator.GenerateInteger(0, 40),
        cholesterol: DataGenerator.GenerateInteger(0, 40),
        ingredients: MockIngredientList(recipeId),
        steps: MockStepList(recipeId, 5),
        meats: MockMeatList(2),
        categories: MockCategoryList(2),
        notes: [],
        source: DataGenerator.GenerateString(30),
        sourceUrl: DataGenerator.GenerateString(30),
        dateCreated: new Date(),
        dateUpdated: new Date(),
        userAccountId: MockUserAccount.id,
        userAccount: MockUserAccount,
    };
};

export const MockRecipeList = (length = 10): Recipe[] => {
    const recipes: Recipe[] = [];

    for (let i = 0; i < length; i += 1) {
        recipes.push(MockRecipe());
    }

    return recipes;
};

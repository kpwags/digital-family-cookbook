import DataGenerator from '@test/DataGenerator';
import Ingredient from '@models/Ingredient';

export const MockIngredient = (recipeId: number): Ingredient => ({
    id: DataGenerator.GenerateGuid(),
    ingredientId: DataGenerator.GenerateInteger(1, 250),
    name: DataGenerator.GenerateString(),
    recipeId,
    sortOrder: DataGenerator.GenerateInteger(1, 20),
});

export const MockIngredientList = (recipeId: number, length = 10): Ingredient[] => {
    const ingredients: Ingredient[] = [];

    for (let i = 0; i < length; i += 1) {
        ingredients.push(MockIngredient(recipeId));
    }

    return ingredients;
};

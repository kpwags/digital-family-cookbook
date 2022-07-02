import DataGenerator from '@test/DataGenerator';
import Ingredient from '@models/Ingredient';

export const MockIngredient = (recipeId: number, id = 0): Ingredient => ({
    id: DataGenerator.GenerateGuid(),
    ingredientId: id === 0 ? DataGenerator.GenerateInteger(1, 250) : id,
    name: DataGenerator.GenerateString(),
    recipeId,
    sortOrder: DataGenerator.GenerateInteger(1, 20),
});

export const MockIngredientList = (recipeId: number, length = 10): Ingredient[] => {
    const ingredients: Ingredient[] = [];

    for (let i = 0; i < length; i += 1) {
        ingredients.push(MockIngredient(recipeId, i + 1));
    }

    return ingredients;
};

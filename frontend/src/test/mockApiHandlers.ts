/* eslint-disable no-case-declarations */
/* eslint-disable import/no-extraneous-dependencies */
import { rest } from 'msw';
import { MockAdminUserAccount, MockUserAccount } from '@test/mocks/MockUsers';
import AuthResult from '@models/AuthResult';
import Recipe from '@models/Recipe';
import copyObject from '@utils/copyObject';
import { emptyQuillField } from '@utils/constants';
import { MockAdminRole, MockUserRole } from './mocks/MockRoleType';
import { MockCategoryList } from './mocks/MockCategory';
import DataGenerator from './DataGenerator';
import { MockMeatList } from './mocks/MockMeat';
import { MockRecipeList } from './mocks/MockRecipe';
import { MockIngredientList } from './mocks/MockIngredient';
import { MockStepList } from './mocks/MockStep';

const mockApiHandlers = [
    // auth controller actions
    rest.get('*/auth/getuser', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json(MockAdminUserAccount),
    )),

    rest.post('*/auth/login', (req, res, ctx) => {
        const { email, password } = req.body as { email: string, password: string };

        if (email === 'test@testing.com' && password === 'validPassword123') {
            const authResult: AuthResult = {
                isSuccessful: true,
                error: '',
                accessToken: '1234567890',
            };

            return res(
                ctx.status(200),
                ctx.json(authResult),
            );
        }
        return res(
            ctx.status(400),
            ctx.text('Unable to verify username or password'),
        );
    }),

    rest.post('*/auth/register', (req, res, ctx) => {
        const { invitationCode } = req.body as { invitationCode: string };

        if (invitationCode === 'please') {
            return res(
                ctx.status(200),
                ctx.json({
                    isSuccessful: true,
                    error: '',
                    accessToken: '1234567890',
                    refreshToken: '123456780',
                }),
            );
        }

        return res(
            ctx.status(400),
            ctx.text('Invalid invitation code'),
        );
    }),

    // system controller actions
    rest.get('*/system/getroles', (req, res, ctx) => res(
        ctx.status(200),
        ctx.json([
            MockAdminRole,
            MockUserRole,
        ]),
    )),

    rest.get('*/system/getusers', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json([
            MockAdminUserAccount,
            MockUserAccount,
        ]),
    )),

    rest.get('*/system/getrolebyid', (req, res, ctx) => {
        const id = req.url.searchParams.get('id');

        if (id === 'admin') {
            return res(
                ctx.status(200),
                ctx.json(MockAdminRole),
            );
        }

        if (id === 'user') {
            return res(
                ctx.status(200),
                ctx.json(MockUserRole),
            );
        }

        return res(
            ctx.status(400),
            ctx.text('Unable to find role'),
        );
    }),

    rest.post('*/system/saverole', (req, res, ctx) => {
        const { name } = req.body as { name: string };

        if (['administrator', 'user'].includes(name.toLowerCase())) {
            return res(
                ctx.status(400),
                ctx.text('Role already exists'),
            );
        }

        return res(ctx.status(200));
    }),

    // category controller actions
    rest.get('*/categories/getall', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json(MockCategoryList()),
    )),

    rest.get('*/categories/get', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json({
            categoryId: 1,
            name: 'Meat',
            id: DataGenerator.GenerateGuid(),
        }),
    )),

    rest.post('*/categories/create', (_, res, ctx) => res(
        ctx.status(200),
    )),

    rest.patch('*/categories/update', (_, res, ctx) => res(
        ctx.status(200),
    )),

    // meat controller actions
    rest.get('*/meats/getall', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json(MockMeatList()),
    )),

    rest.get('*/meats/get', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json({
            meatId: 1,
            name: 'Meat',
            id: DataGenerator.GenerateGuid(),
        }),
    )),

    rest.post('*/meats/create', (_, res, ctx) => res(
        ctx.status(200),
    )),

    rest.patch('*/meats/update', (_, res, ctx) => res(
        ctx.status(200),
    )),

    // recipes controller actions
    rest.get('*/recipes/getall', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json(MockRecipeList(20)),
    )),

    rest.get('*/recipes/getuserrecipes', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json(MockRecipeList(20)),
    )),

    rest.get('*/recipes/get', (req, res, ctx) => {
        const id = req.url.searchParams.get('id');

        if (!id) {
            return res(
                ctx.status(400),
                ctx.text('Unable to find recipe'),
            );
        }

        const recipeId = parseInt(id, 10);

        const baseRecipe: Recipe = {
            recipeId: 1,
            id: DataGenerator.GenerateGuid(),
            name: 'Test Recipe 01',
            description: '<p>This is a delicious recipe</p>',
            servings: 4,
            time: 60,
            activeTime: 20,
            isPublic: true,
            imageUrl: '',
            imageUrlLarge: '',
            imageData: '',
            largeImageData: '',
            calories: 320,
            protein: 15,
            carbohydrates: 20,
            fat: 6,
            sugar: 2,
            fiber: 21,
            cholesterol: 423,
            ingredients: MockIngredientList(1),
            steps: MockStepList(1, 5),
            meats: MockMeatList(2),
            categories: MockCategoryList(2),
            notes: [],
            source: 'AllRecipes',
            sourceUrl: 'https://www.allrecipes.com',
            dateCreated: new Date(),
            dateUpdated: new Date(),
            userAccountId: MockUserAccount.id,
            userAccount: MockUserAccount,
            isFavorite: false,
        };

        switch (recipeId) {
            case 1:
                return res(
                    ctx.status(200),
                    ctx.json(baseRecipe),
                );

            case 2:
                // no description
                const noDescriptionRecipe = copyObject<Recipe>(baseRecipe);
                noDescriptionRecipe.description = emptyQuillField;

                return res(
                    ctx.status(200),
                    ctx.json(noDescriptionRecipe),
                );

            case 3:
                // no nutrition
                const noNutritionRecipe = copyObject<Recipe>(baseRecipe);
                noNutritionRecipe.calories = null;
                noNutritionRecipe.protein = null;
                noNutritionRecipe.carbohydrates = null;
                noNutritionRecipe.fat = null;
                noNutritionRecipe.sugar = null;
                noNutritionRecipe.fiber = null;
                noNutritionRecipe.cholesterol = null;

                return res(
                    ctx.status(200),
                    ctx.json(noNutritionRecipe),
                );

            case 4:
                // with image
                const recipeWithImage = copyObject<Recipe>(baseRecipe);
                recipeWithImage.imageUrlLarge = DataGenerator.GenerateGuid();
                recipeWithImage.largeImageData = DataGenerator.GenerateGuid();

                return res(
                    ctx.status(200),
                    ctx.json(recipeWithImage),
                );

            default:
                return res(
                    ctx.status(400),
                    ctx.text('Unable to find recipe'),
                );
        }
    }),

    rest.get('*/recipes/getrecipesbycategory', (req, res, ctx) => {
        const id = req.url.searchParams.get('categoryId');

        if (!id) {
            return res(
                ctx.status(400),
                ctx.text('Category not specified'),
            );
        }

        const categoryId = parseInt(id, 10);

        switch (categoryId) {
            case 1:
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageTitle: 'Mock Category',
                        recipes: MockRecipeList(8),
                        pageCount: 1,
                        totalRecipeCount: 8,
                    }),
                );
            default:
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageTitle: 'Mock Category',
                        recipes: [],
                        pageCount: 1,
                        totalRecipeCount: 0,
                    }),
                );
        }
    }),

    rest.get('*/recipes/getrecipesbymeat', (req, res, ctx) => {
        const id = req.url.searchParams.get('meatId');

        if (!id) {
            return res(
                ctx.status(400),
                ctx.text('Meat not specified'),
            );
        }

        const meatId = parseInt(id, 10);

        switch (meatId) {
            case 1:
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageTitle: 'Mock Meat',
                        recipes: MockRecipeList(8),
                    }),
                );
            default:
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageTitle: 'Mock Meat',
                        recipes: [],
                    }),
                );
        }
    }),

    rest.get('*/recipes/getrecipesbyuser', (req, res, ctx) => {
        const id = req.url.searchParams.get('userAccountId');

        if (!id) {
            return res(
                ctx.status(400),
                ctx.text('Meat not specified'),
            );
        }

        switch (id) {
            case '123456':
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageTitle: 'Mock User',
                        recipes: MockRecipeList(8),
                    }),
                );
            default:
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageTitle: 'Mock User',
                        recipes: [],
                    }),
                );
        }
    }),

    rest.get('*/recipes/getrecentrecipes', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json(MockRecipeList(8)),
    )),

    rest.get('*/recipes/getmostfavoritedrecipes', (_, res, ctx) => res(
        ctx.status(200),
        ctx.json(MockRecipeList(8)),
    )),

    rest.get('*/recipes/search', (req, res, ctx) => {
        const keywords = req.url.searchParams.get('keywords');

        if (!keywords) {
            return res(
                ctx.status(400),
                ctx.text('Keywords not specified'),
            );
        }

        switch (keywords) {
            case 'noresults':
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageCount: 1,
                        totalRecipeCount: 0,
                        pageTitle: "Search Results for 'noresults'",
                        recipes: [],
                    }),
                );
            default:
                return res(
                    ctx.status(200),
                    ctx.json({
                        pageCount: 1,
                        totalRecipeCount: 8,
                        pageTitle: `Search Results for '${keywords}'`,
                        recipes: MockRecipeList(8),
                    }),
                );
        }
    }),
];

export { mockApiHandlers };

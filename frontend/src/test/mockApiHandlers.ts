/* eslint-disable no-case-declarations */
/* eslint-disable import/no-extraneous-dependencies */
import { rest } from 'msw';
import { MockAdminUserAccount, MockUserAccount } from '@test/mocks/MockUsers';
import { AuthResult } from '@models/AuthResult';
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
                isSuccesful: true,
                error: '',
                token: '1234567890',
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
                    isSuccesful: true,
                    error: '',
                    token: '1234567890',
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
];

export { mockApiHandlers };

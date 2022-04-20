/* eslint-disable import/no-extraneous-dependencies */
import { rest } from 'msw';
import { MockAdminUserAccount, MockUserAccount } from '@test/mocks/MockUsers';
import { AuthResult } from '@models/AuthResult';
import { MockAdminRole, MockUserRole } from './mocks/MockRoleType';
import { MockCategoryList } from './mocks/MockCategory';
import DataGenerator from './DataGenerator';
import { MockMeatList } from './mocks/MockMeat';

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
];

export { mockApiHandlers };

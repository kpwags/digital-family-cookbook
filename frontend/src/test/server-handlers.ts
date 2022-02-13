/* eslint-disable import/no-extraneous-dependencies */
import { rest } from 'msw';
import { MockAdminUserAccount } from '@test/mocks/MockUsers';
import { AuthResult } from '@models/AuthResult';
import { MockAdminRole, MockUserRole } from './mocks/MockRoleType';

const handlers = [
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

    rest.post('*/auth/register', (req, res, ctx) => res(
        ctx.status(200),
        ctx.json({
            isSuccesful: true,
            error: '',
            token: '1234567890',
        }),
    )),

    // system controller actions
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
];

export { handlers };

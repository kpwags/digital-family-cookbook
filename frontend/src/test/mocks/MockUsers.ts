import { v4 as uuid4 } from 'uuid';
import { UserAccount } from '@models/UserAccount';
import { MockAdminRole, MockUserRole } from './MockRoleType';

export const MockAdminUserAccount: UserAccount = {
    id: uuid4(),
    userId: uuid4(),
    email: 'testadmin@toEditorSettings.com',
    name: 'Admin User',
    roles: [
        MockAdminRole,
        MockUserRole,
    ],
};

export const MockUserAccount: UserAccount = {
    id: uuid4(),
    userId: uuid4(),
    email: 'user@toEditorSettings.com',
    name: 'Regular User',
    roles: [
        MockUserRole,
    ],
};

import { v4 as uuid4 } from 'uuid';
import { RoleType } from '@models/RoleType';

export const MockAdminRole: RoleType = {
    id: uuid4(),
    roleTypeId: uuid4(),
    name: 'Administrator',
    normalizedName: 'ADMINISTRATOR',
};

export const MockUserRole: RoleType = {
    id: uuid4(),
    roleTypeId: uuid4(),
    name: 'User',
    normalizedName: 'USER',
};

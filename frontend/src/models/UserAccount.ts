import { RoleType } from './RoleType';

export interface UserAccount {
    id: string
    userId: string
    name: string
    email: string
    roles: RoleType[]
}

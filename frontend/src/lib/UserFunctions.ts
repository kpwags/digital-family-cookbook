import { UserAccount } from '@models/UserAccount';

export const hasRole = (user: UserAccount, roleName: string): boolean => user.roles.filter((u) => u.normalizedName === roleName).length > 0;

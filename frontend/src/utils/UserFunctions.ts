import { UserAccount } from '@models/UserAccount';

export const hasRole = (user: UserAccount | null, roleName: string): boolean => {
    if (user) {
        return user.roles.filter((u) => u.normalizedName === roleName).length > 0;
    }

    return false;
};

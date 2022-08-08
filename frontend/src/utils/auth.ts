import AuthResult from '@models/AuthResult';
import { Api } from './api';
import LocalStorageUtils from './LocalStorageUtils';

const getNewRefreshToken = async (): Promise<[AuthResult | null, string | null]> => {
    const [data, error] = await Api.Post<AuthResult>(
        'auth/refreshtoken',
        {
            isRefreshTokenRequest: true,
            credentials: 'include',
        },
    );

    if (error || data === null) {
        return [null, error || 'Error refreshing token'];
    }

    LocalStorageUtils.setAccessToken(data.accessToken);

    return [data, null];
};

export { getNewRefreshToken };

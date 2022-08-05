import AuthResult from '@models/AuthResult';
import { Api } from './api';
import LocalStorageUtils from './LocalStorageUtils';

const getNewRefreshToken = async (): Promise<[AuthResult | null, string | null]> => {
    const token = LocalStorageUtils.getRefreshToken();

    const [data, error] = await Api.Post<AuthResult>(
        'auth/refreshtoken',
        {
            data: {
                token,
            },
            isRefreshTokenRequest: true,
        },
    );

    if (error || data === null) {
        return [null, error || 'Error refreshing token'];
    }

    LocalStorageUtils.setAccessToken(data.accessToken);
    LocalStorageUtils.setRefreshToken(data.refreshToken);

    return [data, null];
};

export { getNewRefreshToken };

import { ApiArguments } from '@models/ApiArguments';
import { client, handleApiError } from '@utils/client';
import { getNewRefreshToken } from './auth';
import LocalStorageUtils from './LocalStorageUtils';

class Api {
    static Post<T>(endpoint: string, config: ApiArguments = {}) : Promise<[T | null, string | null]> {
        const apiConfig: ApiArguments = {
            method: 'POST',
            ...config,
        };

        return client(endpoint, apiConfig).then(
            (data) => [data, null],
            async (error) => {
                const errorMessage = handleApiError(error);

                if (errorMessage === 'TOKEN_EXPIRED' && !config.isRefreshTokenRequest) {
                    const [data] = await getNewRefreshToken();

                    if (data && data.isSuccessful) {
                        LocalStorageUtils.setAccessToken(data.accessToken);
                    }

                    return client(endpoint, apiConfig).then(
                        (data) => [data, null],
                        (error) => [null, handleApiError(error)],
                    );
                }

                return [null, handleApiError(error)];
            },
        );
    }

    static PostWithUpload<T>(endpoint: string, config: ApiArguments = {}) : Promise<[T | null, string | null]> {
        const apiConfig: ApiArguments = {
            method: 'POST',
            ...config,
            contentType: null,
            fileUpload: true,
        };

        return client(endpoint, apiConfig).then(
            (data) => [data, null],
            async (error) => {
                const errorMessage = handleApiError(error);

                if (errorMessage === 'TOKEN_EXPIRED' && !config.isRefreshTokenRequest) {
                    const [data] = await getNewRefreshToken();

                    if (data && data.isSuccessful) {
                        LocalStorageUtils.setAccessToken(data.accessToken);
                    }

                    return client(endpoint, apiConfig).then(
                        (data) => [data, null],
                        (error) => [null, handleApiError(error)],
                    );
                }

                return [null, handleApiError(error)];
            },
        );
    }

    static Get<T>(endpoint: string, config: ApiArguments = { params: {} }) : Promise<[T | null, string | null]> {
        let url = `${endpoint}?`;

        if (config.params) {
            Object.keys(config.params).forEach((key) => {
                url += `${key}=${config.params[key]}&`;
            });
        }

        url = url.substring(0, url.length - 1);

        return client(url, { method: 'GET' }).then(
            (data) => [data, null],
            async (error) => {
                const errorMessage = handleApiError(error);

                if (errorMessage === 'TOKEN_EXPIRED' && !config.isRefreshTokenRequest) {
                    const [data] = await getNewRefreshToken();

                    if (data && data.isSuccessful) {
                        LocalStorageUtils.setAccessToken(data.accessToken);
                    }

                    return client(url, { method: 'GET' }).then(
                        (data) => [data, null],
                        (error) => [null, handleApiError(error)],
                    );
                }

                return [null, handleApiError(error)];
            },
        );
    }

    static Delete<T>(endpoint: string, config: ApiArguments = { params: {} }) : Promise<[T | null, string | null]> {
        let url = `${endpoint}?`;

        if (config.params) {
            Object.keys(config.params).forEach((key) => {
                url += `${key}=${config.params[key]}&`;
            });
        }

        url = url.substring(0, url.length - 1);

        return client(url, { method: 'DELETE' }).then(
            (data) => [data, null],
            async (error) => {
                const errorMessage = handleApiError(error);

                if (errorMessage === 'TOKEN_EXPIRED' && !config.isRefreshTokenRequest) {
                    const [data] = await getNewRefreshToken();

                    if (data && data.isSuccessful) {
                        LocalStorageUtils.setAccessToken(data.accessToken);
                    }

                    return client(endpoint, { method: 'DELETE' }).then(
                        (data) => [data, null],
                        (error) => [null, handleApiError(error)],
                    );
                }

                return [null, handleApiError(error)];
            },
        );
    }

    static Patch<T>(endpoint: string, config: ApiArguments = {}) : Promise<[T | null, string | null]> {
        const apiConfig: ApiArguments = {
            method: 'PATCH',
            ...config,
        };

        return client(endpoint, apiConfig).then(
            (data) => [data, null],
            async (error) => {
                const errorMessage = handleApiError(error);

                if (errorMessage === 'TOKEN_EXPIRED' && !config.isRefreshTokenRequest) {
                    const [data] = await getNewRefreshToken();

                    if (data && data.isSuccessful) {
                        LocalStorageUtils.setAccessToken(data.accessToken);
                    }

                    return client(endpoint, apiConfig).then(
                        (data) => [data, null],
                        (error) => [null, handleApiError(error)],
                    );
                }

                return [null, handleApiError(error)];
            },
        );
    }
}

export { Api };

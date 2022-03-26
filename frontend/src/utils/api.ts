import { ApiArguments } from '@models/ApiArguments';
import { client, handleApiError } from '@utils/client';

class Api {
    static Post<T>(endpoint: string, config: ApiArguments = {}) : Promise<[T | null, string | null]> {
        return client(endpoint, config).then(
            (data) => [data, null],
            (error) => [null, handleApiError(error)],
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

        return client(url, { token: config.token }).then(
            (data) => [data, null],
            (error) => [null, handleApiError(error)],
        );
    }
}

export { Api };

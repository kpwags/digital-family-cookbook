import { ApiArguments } from '@models/ApiArguments';
import { client, handleApiError } from '@lib/client';

class Api {
    static Post<T>(endpoint: string, config: ApiArguments = {}) : Promise<[T | null, string | null]> {
        return client(endpoint, config).then(
            (data) => [data, null],
            (error) => [null, handleApiError(error)],
        );
    }

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    static Get<T>(endpoint: string, params: any = {}) : Promise<[T | null, string | null]> {
        let url = `${endpoint}?`;

        Object.keys(params).forEach((key) => {
            url += `${key}=${params[key]}&`;
        });

        url = url.substr(0, url.length - 1);

        return client(url).then(
            (data) => [data, null],
            (error) => [null, handleApiError(error)],
        );
    }
}

export { Api };

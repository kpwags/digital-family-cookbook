import { ApiArguments } from '@models/ApiArguments';
import LocalStorageUtils from './LocalStorageUtils';

const apiUrl = process.env.REACT_APP_API_URL;

async function client(endpoint: string, {
    data = null,
    contentType = 'application/json',
    fileUpload = false,
    method = undefined,
    ...customConfig
// eslint-disable-next-line @typescript-eslint/no-explicit-any
}: ApiArguments = {}) : Promise<any> {
    const headers: Record<string, string> = {};

    const token = LocalStorageUtils.getAccessToken();

    if (token) {
        headers.Authorization = `Bearer ${token}`;
    }

    if (contentType !== null) {
        headers['Content-Type'] = contentType;
    }

    let body;
    if (data && data !== null) {
        body = (fileUpload ? data : JSON.stringify(data));
    }

    let clientMethod = data ? 'POST' : 'GET';
    if (method) {
        clientMethod = method;
    }

    const config: RequestInit = {
        method: clientMethod,
        body,
        headers,
        ...customConfig,
    };

    return window.fetch(`${apiUrl}/${endpoint}`, config).then(async (response) => {
        if (response.status === 401) {
            const data = await response.json() as { message: string };

            if (data.message === 'TOKEN_EXPIRED') {
                return Promise.reject(new Error(data.message));
            }

            LocalStorageUtils.clearAccessToken();
            LocalStorageUtils.clearRefreshToken();

            // refresh the page for them
            window.location.assign(window.location.toString());

            return Promise.reject(new Error('Session Expired'));
        }

        if (response.status === 400) {
            const error = await response.text();
            return Promise.reject(error);
        }

        if (response.ok) {
            const responseContentType = response.headers.get('content-type');

            if (responseContentType && responseContentType.indexOf('application/json') !== -1) {
                const responseData = await response.json();
                return responseData;
            }

            return {};
        }

        return Promise.reject(new Error('Unknown Error'));
    });
}

const handleApiError = (error: string | Error): string => {
    if (typeof error === 'string') {
        return error;
    }

    if (typeof error.message === 'string') {
        return error.message;
    }

    return 'An error has occurred';
};

export {
    client,
    handleApiError,
};

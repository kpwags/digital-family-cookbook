import { ApiArguments } from '@models/ApiArguments';

const apiUrl = process.env.REACT_APP_API_URL;
// const localStorageKey = '__fittracker_token__';

// function logout() {
//     window.localStorage.removeItem(localStorageKey);
// }

async function client(endpoint: string, {
    data = null,
    token = undefined,
    contentType = 'application/json',
    fileUpload = false,
    ...customConfig
// eslint-disable-next-line @typescript-eslint/no-explicit-any
}: ApiArguments = {}) : Promise<any> {
    // const token = window.localStorage.getItem(localStorageKey);

    const headers: Record<string, string> = {};

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

    const config: RequestInit = {
        method: data ? 'POST' : 'GET',
        body,
        headers,
        ...customConfig,
    };

    return window.fetch(`${apiUrl}/${endpoint}`, config).then(async (response) => {
        if (response.status === 401) {
            // logout();

            // refresh the page for them
            window.location.assign(window.location.toString());

            // eslint-disable-next-line prefer-promise-reject-errors
            return Promise.reject({ message: 'Please re-authenticate.' });
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

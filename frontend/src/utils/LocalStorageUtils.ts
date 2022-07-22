class LocalStorageUtils {
    static setAccessToken = (token: string) => window.localStorage.setItem('__dfc_access_token__', token);

    static getAccessToken = (): string | null => window.localStorage.getItem('__dfc_access_token__');

    static setRefreshToken = (token: string) => window.localStorage.setItem('__dfc_refresh_token__', token);

    static getRefreshToken = (): string | null => window.localStorage.getItem('__dfc_refresh_token__');

    static clearAccessToken = () => window.localStorage.removeItem('__dfc_access_token__');

    static clearRefreshToken = () => window.localStorage.removeItem('__dfc_refresh_token__');

    static getValue = (key: string): string | null => window.localStorage.getItem(key);

    static clearValue = (key: string) => window.localStorage.removeItem(key);
}

export default LocalStorageUtils;

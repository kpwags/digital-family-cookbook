const isTokenExpired = (token: string | null): boolean | null => {
    if (token) {
        const tokenVal = window.atob(token.split('.')[1]);

        return Date.now() >= (JSON.parse(tokenVal)).exp * 1000;
    }

    return null;
};

export default isTokenExpired;

class CookieUtils {
    static GetCookie(name: string): boolean {
        return document.cookie.split(';').some((c) => c.trim().startsWith(`${name}=`));
    }

    static DeleteCookie(name: string, path = '/', domain = ''): void {
        if (this.GetCookie(name)) {
            document.cookie = `${name}=${
                (path) ? `;path=${path}` : ''
            }${(domain) ? `;domain=${domain}` : ''
            };expires=Thu, 01 Jan 1970 00:00:01 GMT`;
        }
    }
}

export { CookieUtils };

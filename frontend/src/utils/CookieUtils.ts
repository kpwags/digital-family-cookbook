class CookieUtils {
    static GetValue(name: string): string | null {
        const cookieName = `${name}=`;

        const ca = document.cookie.split(';');

        for (let i = 0; i < ca.length; i += 1) {
            const c = ca[i].trim();

            if ((c.indexOf(cookieName)) === 0) {
                return c.substring(cookieName.length);
            }
        }

        return null;
    }

    static Exists(name: string): boolean {
        return document.cookie.split(';').some((c) => c.trim().startsWith(`${name}=`));
    }

    static DeleteCookie(name: string, path = '/', domain = ''): void {
        if (this.Exists(name)) {
            document.cookie = `${name}=${
                (path) ? `;path=${path}` : ''
            }${(domain) ? `;domain=${domain}` : ''
            };expires=Thu, 01 Jan 1970 00:00:01 GMT`;
        }
    }
}

export { CookieUtils };

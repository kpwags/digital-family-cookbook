import { createContext } from 'react';
import { SiteSettings } from '@models/SiteSettings';
import { UserAccount } from '@models/UserAccount';
import { defaultSiteSettings } from '@utils/defaults';
import { Category } from '@models/Category';

type AppContextProps = {
    siteSettings: SiteSettings
    token: string | undefined
    user: UserAccount | null
    categories: Category[]
    updateSiteSettings: (settings: SiteSettings) => void
    loginUser: (token: string) => void
    logout: () => void
    refreshUser: () => void
    updateCategories: (c: Category[]) => void
}

const AppContext = createContext<AppContextProps>({
    siteSettings: defaultSiteSettings,
    token: undefined,
    user: null,
    categories: [],
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    loginUser: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    logout: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    refreshUser: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    updateSiteSettings: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    updateCategories: () => { },
});

export default AppContext;

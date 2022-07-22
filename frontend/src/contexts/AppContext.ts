import { createContext } from 'react';
import { SiteSettings } from '@models/SiteSettings';
import { UserAccount } from '@models/UserAccount';
import { defaultSiteSettings } from '@utils/defaults';
import { Category } from '@models/Category';
import { Meat } from '@models/Meat';

type AppContextProps = {
    siteSettings: SiteSettings
    token: string | null
    user: UserAccount | null
    categories: Category[]
    meats: Meat[]
    updateSiteSettings: (settings: SiteSettings) => void
    loginUser: (accessToken: string, refreshToken: string) => void
    logout: () => void
    refreshUser: () => void
    updateCategories: (c: Category[]) => void
    updateMeats: (m: Meat[]) => void
}

const AppContext = createContext<AppContextProps>({
    siteSettings: defaultSiteSettings,
    token: null,
    user: null,
    categories: [],
    meats: [],
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
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    updateMeats: () => { },
});

export default AppContext;

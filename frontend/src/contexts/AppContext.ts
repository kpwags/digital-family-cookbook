import { createContext } from 'react';
import { SiteSettings } from '@models/SiteSettings';
import { UserAccount } from '@models/UserAccount';

type AppContextProps = {
    siteSettings: SiteSettings
    token: string | undefined
    user: UserAccount | null
    loginUser: (token: string) => void
    logout: () => void
    refreshUser: () => void
}

const AppContext = createContext<AppContextProps>({
    siteSettings: {
        id: '1',
        siteSettingsId: 1,
        title: 'Digital Family Cookbook',
        isPublic: false,
    },
    token: undefined,
    user: null,
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    loginUser: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    logout: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    refreshUser: () => { },
});

export { AppContext };

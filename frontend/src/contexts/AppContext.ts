import { createContext } from 'react';
import { SiteSettings } from '@models/SiteSettings';
import { UserAccount } from '@models/UserAccount';
import { defaultSiteSettings } from '@utils/defaults';

type AppContextProps = {
    siteSettings: SiteSettings
    token: string | undefined
    user: UserAccount | null
    updateSiteSettings: (settings: SiteSettings) => void
    loginUser: (token: string) => void
    logout: () => void
    refreshUser: () => void
}

const AppContext = createContext<AppContextProps>({
    siteSettings: defaultSiteSettings,
    token: undefined,
    user: null,
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    loginUser: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    logout: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    refreshUser: () => { },
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    updateSiteSettings: () => { },
});

export { AppContext };

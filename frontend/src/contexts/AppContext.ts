import { createContext } from 'react';
import { SiteSettings } from '@models/SiteSettings';
import { UserAccount } from '@models/UserAccount';

type AppContextProps = {
    siteSettings: SiteSettings
    token: string | undefined
    user: UserAccount | null
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
});

export { AppContext };

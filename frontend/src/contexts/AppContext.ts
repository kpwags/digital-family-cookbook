import { createContext } from 'react';
import { SiteSettings } from '@models/SiteSettings';

type AppContextProps = {
    siteSettings: SiteSettings
}

const AppContext = createContext<AppContextProps>({
    siteSettings: {
        id: '1',
        siteSettingsId: 1,
        title: 'Digital Family Cookbook',
        isPublic: false,
    },
});

export { AppContext };

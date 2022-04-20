import { ReactNode } from 'react';
import AppContext from '@contexts/AppContext';
import { UserAccount } from '@models/UserAccount';
import { SiteSettings } from '@models/SiteSettings';
import { defaultSiteSettings } from '@utils/defaults';
import copyObject from '@utils/copyObject';
import { Category } from '@models/Category';
import { Meat } from '@models/Meat';

interface MockAppProviderProps {
    siteSettings?: SiteSettings
    user?: UserAccount | null
    token?: string | undefined
    categories?: Category[]
    meats?: Meat[]
    updateSiteSettings?: (settings: SiteSettings) => void
    loginUser?: (token: string) => void
    logout?: () => void
    refreshUser?: () => void
    updateCategories?: () => void
    updateMeats?: () => void
    children: ReactNode
}

const mockSiteSettings = copyObject(defaultSiteSettings);
mockSiteSettings.invitationCode = 'dfa26202-1f0a-4be6-a326-9f675cd992bf';
mockSiteSettings.isPublic = true;

const MockAppProvider = ({
    siteSettings = mockSiteSettings,
    user = null,
    token = undefined,
    categories = [],
    meats = [],
    updateSiteSettings = jest.fn(),
    loginUser = jest.fn(),
    logout = jest.fn(),
    refreshUser = jest.fn(),
    updateCategories = jest.fn(),
    updateMeats = jest.fn(),
    children,
}: MockAppProviderProps) => (
    <AppContext.Provider
        // eslint-disable-next-line react/jsx-no-constructed-context-values
        value={{
            siteSettings,
            user,
            categories,
            meats,
            token,
            loginUser,
            logout,
            refreshUser,
            updateSiteSettings,
            updateCategories,
            updateMeats,
        }}
    >
        {children}
    </AppContext.Provider>
);

export { MockAppProvider };

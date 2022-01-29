import { ReactNode } from 'react';
import { AppContext } from '@contexts/AppContext';
import { UserAccount } from '@models/UserAccount';
import { SiteSettings } from '@models/SiteSettings';

interface MockAppProviderProps {
    siteSettings?: SiteSettings
    user?: UserAccount | null
    token?: string | undefined
    loginUser?: (token: string) => void
    logout?: () => void
    refreshUser?: () => void
    children: ReactNode
}

const MockAppProvider = ({
    siteSettings = {
        id: '1',
        siteSettingsId: 1,
        title: 'Digital Family Cookbook',
        isPublic: false,
    },
    user = null,
    token = undefined,
    loginUser = jest.fn(),
    logout = jest.fn(),
    refreshUser = jest.fn(),
    children,
}: MockAppProviderProps) => (
    <AppContext.Provider
        // eslint-disable-next-line react/jsx-no-constructed-context-values
        value={{
            siteSettings,
            user,
            token,
            loginUser,
            logout,
            refreshUser,
        }}
    >
        {children}
    </AppContext.Provider>
);

export { MockAppProvider };

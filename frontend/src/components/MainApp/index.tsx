import { SiteSettings } from '@models/SiteSettings';
import {
    ReactNode,
    useState,
    useEffect,
} from 'react';
import { AppContext } from '@contexts/AppContext';
import { Api } from '@lib/api';
import { PageState } from '@lib/enums';
import { useCookies } from 'react-cookie';
import { UserAccount } from '@models/UserAccount';

const MainApp = ({ children }: { children: ReactNode }): JSX.Element => {
    const [siteSettingsLoaded, setSiteSettingsLoaded] = useState<boolean>(false);
    const [siteSettings, setSiteSettings] = useState<SiteSettings>({
        id: '1',
        siteSettingsId: 1,
        title: 'Digital Family Cookbook',
        isPublic: false,
    });
    const [user, setUser] = useState<UserAccount | null>(null);
    const [pageError, setPageError] = useState<string>('');
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [cookies] = useCookies(['dfcuser']);

    const loadUser = async () => {
        const [data, error] = await Api.Get<UserAccount>('auth/getuser', { token: cookies.dfcuser });

        if (error || data === null) {
            setPageError(error || 'Unable to load site settings');
            setPageState(PageState.Error);
            return;
        }

        setUser(data);
        setPageState(PageState.Ready);
    };

    const loadSiteSettings = async () => {
        setSiteSettingsLoaded(true);
        const [data, error] = await Api.Get<SiteSettings>('system/getsitesettings');

        if (error || data === null) {
            setPageError(error || 'Unable to load site settings');
            setPageState(PageState.Error);
            return;
        }

        if (cookies.dfcuser) {
            loadUser();
        } else {
            setSiteSettings(data);
            setPageState(PageState.Ready);
        }
    };

    useEffect(() => {
        if (!siteSettingsLoaded) {
            loadSiteSettings();
        }
    }, []);

    if (pageState === PageState.Loading) {
        return <></>;
    }

    if (pageState === PageState.Error) {
        return <>{pageError}</>;
    }

    return (
        <AppContext.Provider
            // eslint-disable-next-line react/jsx-no-constructed-context-values
            value={{
                siteSettings,
                token: cookies.dfcuser,
                user,
            }}
        >
            {children}
        </AppContext.Provider>
    );
};

export { MainApp };

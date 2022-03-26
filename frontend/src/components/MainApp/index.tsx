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
import { defaultSiteSettings } from '@lib/defaults';

const MainApp = ({ children }: { children: ReactNode }): JSX.Element => {
    const [siteSettingsLoaded, setSiteSettingsLoaded] = useState<boolean>(false);
    const [siteSettings, setSiteSettings] = useState<SiteSettings>(defaultSiteSettings);
    const [user, setUser] = useState<UserAccount | null>(null);
    const [pageError, setPageError] = useState<string>('');
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [cookies, setCookie, removeCookie] = useCookies(['dfcuser']);

    const loadUser = async (token: string | undefined = undefined) => {
        const [data, error] = await Api.Get<UserAccount>('auth/getuser', { token: token || cookies.dfcuser });

        if (error || data === null) {
            setPageError(error || 'Unable to load site settings');
            setPageState(PageState.Error);
            return;
        }

        setUser(data);
        setPageState(PageState.Ready);
    };

    const logout = () => {
        removeCookie('dfcuser');
        document.location.reload();
    };

    const loadSiteSettings = async () => {
        setSiteSettingsLoaded(true);
        const [data, error] = await Api.Get<SiteSettings>('public/getsitesettings');

        if (error || data === null) {
            setPageError(error || 'Unable to load site settings');
            setPageState(PageState.Error);
            return;
        }

        if (cookies.dfcuser) {
            loadUser();
            setSiteSettings(data);
            setPageState(PageState.Ready);
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

    const refreshUser = () => {
        if (cookies.dfcuser) {
            loadUser();
        }
    };

    const loginUser = (token: string) => {
        setCookie('dfcuser', token, { path: '/' });
        loadUser(token);
    };

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
                logout,
                refreshUser,
                loginUser,
                updateSiteSettings: (settings) => {
                    setSiteSettings(settings);
                },
            }}
        >
            {children}
        </AppContext.Provider>
    );
};

export { MainApp };

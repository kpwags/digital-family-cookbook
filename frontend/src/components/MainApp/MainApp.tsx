import {
    ReactNode,
    useState,
    useEffect,
    useRef,
    useCallback,
} from 'react';
import AppContext from '@contexts/AppContext';
import { Api } from '@utils/api';
import { PageState } from '@utils/constants';
import { defaultSiteSettings } from '@utils/defaults';
import { SiteSettings } from '@models/SiteSettings';
import { UserAccount } from '@models/UserAccount';
import { Category } from '@models/Category';
import { Meat } from '@models/Meat';
import { getNewRefreshToken } from '@utils/auth';
import LocalStorageUtils from '@utils/LocalStorageUtils';
import { Button } from 'antd';

const MainApp = ({ children }: { children: ReactNode }): JSX.Element => {
    const [siteSettings, setSiteSettings] = useState<SiteSettings>(defaultSiteSettings);
    const [user, setUser] = useState<UserAccount | null>(null);
    const [pageError, setPageError] = useState<string>('');
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [categories, setCategories] = useState<Category[]>([]);
    const [meats, setMeats] = useState<Meat[]>([]);

    const intervalRef = useRef<NodeJS.Timer>();

    const logout = () => {
        LocalStorageUtils.clearAccessToken();
        LocalStorageUtils.clearRefreshToken();
        document.location.reload();
    };

    const loadUser = async () => {
        const [data, error] = await Api.Get<UserAccount>('auth/getuser');

        if (error || data === null) {
            logout();
            setPageState(PageState.Ready);
            return;
        }

        setUser(data);
        setPageState(PageState.Ready);
    };

    const loadSiteData = async () => {
        const [
            [siteSettingsData, siteSettingsError],
            [categoriesData, categoriesError],
            [meatsData, meatsError],
        ] = await Promise.all([
            Api.Get<SiteSettings>('public/getsitesettings'),
            Api.Get<Category[]>('categories/getall'),
            Api.Get<Meat[]>('meats/getall'),
        ]);

        if (siteSettingsError || categoriesError || meatsError || !siteSettingsData || !categoriesData || !meatsData) {
            setPageError(siteSettingsError || categoriesError || meatsError || 'An error has occured');
            setPageState(PageState.Error);
            return;
        }

        setCategories(categoriesData);
        setMeats(meatsData);

        if (LocalStorageUtils.getAccessToken()) {
            loadUser();
            setSiteSettings(siteSettingsData);
            setPageState(PageState.Ready);
        } else {
            setSiteSettings(siteSettingsData);
            setPageState(PageState.Ready);
        }
    };

    useEffect(() => {
        loadSiteData();
    }, []);

    const getToken = useCallback(async () => {
        // Get new token if and only if existing token is available
        if (LocalStorageUtils.getAccessToken() != null) {
            const [data] = await getNewRefreshToken();

            if (data && data.isSuccessful) {
                LocalStorageUtils.setAccessToken(data.accessToken);
                LocalStorageUtils.setRefreshToken(data.refreshToken);
            }
        }
    }, []);

    useEffect(() => {
        const interval = setInterval(() => getToken(), 450000); // 7.5 minutes
        intervalRef.current = interval;
        return () => clearInterval(interval);
    }, [getToken]);

    const refreshUser = () => {
        if (LocalStorageUtils.getAccessToken()) {
            loadUser();
        }
    };

    const loginUser = (accessToken: string, refreshToken: string) => {
        LocalStorageUtils.setAccessToken(accessToken);
        LocalStorageUtils.setRefreshToken(refreshToken);
        loadUser();
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
                token: LocalStorageUtils.getAccessToken(),
                user,
                categories,
                meats,
                logout,
                refreshUser,
                loginUser,
                updateSiteSettings: (settings) => {
                    setSiteSettings(settings);
                },
                updateCategories: (c) => {
                    setCategories(c);
                },
                updateMeats: (m) => {
                    setMeats(m);
                },
            }}
        >
            {user ? <Button onClick={() => getToken()}>Refresh Tokens</Button> : null}
            {children}
        </AppContext.Provider>
    );
};

export default MainApp;

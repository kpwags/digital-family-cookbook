import {
    ReactNode,
    useState,
    useEffect,
} from 'react';
import { useCookies } from 'react-cookie';
import AppContext from '@contexts/AppContext';
import { Api } from '@utils/api';
import { PageState } from '@utils/constants';
import { defaultSiteSettings } from '@utils/defaults';
import { SiteSettings } from '@models/SiteSettings';
import { UserAccount } from '@models/UserAccount';
import { Category } from '@models/Category';
import { Meat } from '@models/Meat';

const MainApp = ({ children }: { children: ReactNode }): JSX.Element => {
    const [siteSettings, setSiteSettings] = useState<SiteSettings>(defaultSiteSettings);
    const [user, setUser] = useState<UserAccount | null>(null);
    const [pageError, setPageError] = useState<string>('');
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [cookies, setCookie, removeCookie] = useCookies(['dfcuser']);
    const [categories, setCategories] = useState<Category[]>([]);
    const [meats, setMeats] = useState<Meat[]>([]);

    const loadUser = async () => {
        const [data, error] = await Api.Get<UserAccount>('auth/getuser');

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

        if (cookies.dfcuser) {
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

    const refreshUser = () => {
        if (cookies.dfcuser) {
            loadUser();
        }
    };

    const loginUser = (token: string) => {
        setCookie('dfcuser', token, { path: '/' });
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
                token: cookies.dfcuser,
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
            {children}
        </AppContext.Provider>
    );
};

export default MainApp;

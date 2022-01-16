import { SiteSettings } from '@models/SiteSettings';
import {
    ReactNode,
    useState,
    useEffect,
} from 'react';
import { AppContext } from '@contexts/AppContext';
import { Api } from '@lib/api';
import { PageState } from '@lib/enums';

const MainApp = ({ children }: { children: ReactNode }): JSX.Element => {
    const [siteSettingsLoaded, setSiteSettingsLoaded] = useState<boolean>(false);
    const [siteSettings, setSiteSettings] = useState<SiteSettings>({
        id: '1',
        siteSettingsId: 1,
        title: 'Digital Family Cookbook',
        isPublic: false,
    });
    const [pageError, setPageError] = useState<string>('');
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);

    const loadSiteSettings = async () => {
        setSiteSettingsLoaded(true);
        const [data, error] = await Api.Get<SiteSettings>('system/getsitesettings');

        if (error || data === null) {
            setPageError(error || 'Unable to load site settings');
            setPageState(PageState.Error);
            return;
        }

        setSiteSettings(data);
        setPageState(PageState.Ready);
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
            }}
        >
            {children}
        </AppContext.Provider>
    );
};

export { MainApp };

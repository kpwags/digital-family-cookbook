import { ReactElement, useContext } from 'react';
import AppContext from '@contexts/AppContext';
import Login from '@components/Pages/Login';

type PrivateFilterProps = {
    children: ReactElement;
    redirectTo?: string;
}

const PrivateFilter = ({
    redirectTo = '/',
    children,
}: PrivateFilterProps): JSX.Element => {
    const { user, siteSettings } = useContext(AppContext);

    if (!siteSettings.isPublic && (!user || user.id === '')) {
        return <Login redirectTo={redirectTo} />;
    }

    return children;
};

export default PrivateFilter;

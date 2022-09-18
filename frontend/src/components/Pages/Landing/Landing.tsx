import PrivateLanding from '@components/PrivateLanding';
import PublicLanding from '@components/PublicLanding';
import AppContext from '@contexts/AppContext';
import useDocumentTitle from '@hooks/useDocumentTitle';
import { useContext } from 'react';

const Landing = (): JSX.Element => {
    const { siteSettings, user } = useContext(AppContext);

    useDocumentTitle(undefined);

    return (
        <>
            {siteSettings.isPublic || ((user && user.id !== '')) ? (
                <PublicLanding />
            ) : (
                <PrivateLanding />
            )}
        </>
    );
};

export default Landing;

import MostFavoritedRecipes from '@components/MostFavoritedRecipes';
import PublicLanding from '@components/PublicLanding';
import AppContext from '@contexts/AppContext';
import useDocumentTitle from '@hooks/useDocumentTitle';
import { useContext } from 'react';

const Landing = (): JSX.Element => {
    const { siteSettings } = useContext(AppContext);

    useDocumentTitle(undefined);

    return (
        <>
            {siteSettings.isPublic ? (
                <>
                    <PublicLanding />
                    <MostFavoritedRecipes />
                </>
            ) : (
                <p>Site is not public!</p>
            )}
        </>
    );
};

export default Landing;

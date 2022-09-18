import { useContext } from 'react';
import MostFavoritedRecipes from '@components/MostFavoritedRecipes';
import MostRecentRecipes from '@components/MostRecentRecipes';
import PrivateLanding from '@components/PrivateLanding';
import AppContext from '@contexts/AppContext';

const PublicLanding = (): JSX.Element => {
    const { user } = useContext(AppContext);

    return (
        <>
            {!user ? <PrivateLanding /> : null}
            <MostRecentRecipes count={8} />
            <MostFavoritedRecipes count={8} />
        </>
    );
};

export default PublicLanding;

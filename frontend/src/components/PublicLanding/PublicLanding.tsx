import MostFavoritedRecipes from '@components/MostFavoritedRecipes';
import MostRecentRecipes from '@components/MostRecentRecipes';

const PublicLanding = (): JSX.Element => (
    <>
        <MostRecentRecipes count={8} />
        <MostFavoritedRecipes count={8} />
    </>
);

export default PublicLanding;

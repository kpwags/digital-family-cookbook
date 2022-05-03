import RecipeForm from '@components/Forms/RecipeForm';
import {
    Typography,
} from 'antd';
import { useContext, useEffect } from 'react';
import AppContext from '@contexts/AppContext';

const { Title } = Typography;

const CreateRecipe = (): JSX.Element => {
    const { siteSettings } = useContext(AppContext);

    useEffect(() => {
        document.title = `Add Recipe - ${siteSettings.title}`;
    }, []);

    return (
        <>
            <Title level={1}>Add Recipe</Title>

            <RecipeForm
                onSave={() => { /* todo */ }}
            />
        </>
    );
};

export default CreateRecipe;

import RecipeForm from '@components/Forms/RecipeForm';
import {
    Typography,
    message,
} from 'antd';
import useDocumentTitle from '@hooks/useDocumentTitle';

const { Title } = Typography;

const CreateRecipe = (): JSX.Element => {
    useDocumentTitle('Add Recipe');

    return (
        <>
            <Title level={1}>Add Recipe</Title>

            <RecipeForm
                onSave={() => { message.success('Recipe created successfully'); }}
            />
        </>
    );
};

export default CreateRecipe;

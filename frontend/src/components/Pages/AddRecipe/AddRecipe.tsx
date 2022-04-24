import RecipeForm from '@components/Forms/RecipeForm';
import { Typography } from 'antd';

const { Title } = Typography;

const CreateRecipe = (): JSX.Element => (
    <>
        <Title level={1}>Add Recipe</Title>
        <RecipeForm
            onSave={() => { /* todo */ }}
        />
    </>
);

export default CreateRecipe;

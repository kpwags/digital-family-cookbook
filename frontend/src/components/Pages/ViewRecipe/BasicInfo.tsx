import Recipe from '@models/Recipe';
import { Space, Typography } from 'antd';
import convertTime from '@utils/convertTime';
import RecipeSource from './RecipeSource';

const { Title, Text } = Typography;

const BasicInfo = ({
    recipe,
}: { recipe: Recipe }): JSX.Element => (
    <Space direction="vertical" className="basic-info">
        <Title level={3}>Basic Info</Title>
        <Text><Text strong>Servings:</Text> {recipe.servings}</Text>
        {(recipe.time || 0) > 0 ? <Text><Text strong>Time:</Text> {convertTime(recipe.time)}</Text> : null}
        {(recipe.activeTime || 0) > 0 ? <Text><Text strong>Active Time:</Text> {convertTime(recipe.activeTime)}</Text> : null}
        <Text><Text strong>Added By:</Text> {recipe.userAccount?.name}</Text>
        <RecipeSource recipe={recipe} />
    </Space>
);

export default BasicInfo;

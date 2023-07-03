import { Button, Space, Typography } from 'antd';

import './PrivateRecipe.less';

const { Title, Paragraph } = Typography;

interface PrivateRecipeProps {
    id: number;
}

const PrivateRecipe = ({ id }: PrivateRecipeProps): JSX.Element => (
    <Space direction="vertical" size={12} className="private-recipe">
        <Title level={1}>Private</Title>
        <Paragraph>Sorry, but the recipe you&apos;re looking for is marked as private. Please sign in to view.</Paragraph>
        <Button type="primary" href={`/login?redirect=/recipes/view/${id}`}>Login</Button>
    </Space>
);

export default PrivateRecipe;

import { useNavigate } from 'react-router';
import { Button, Result } from 'antd';
import { FrownOutlined } from '@ant-design/icons';

type NoRecipesProps = {
    pageText?: string
}

const NoRecipes = ({
    pageText,
}: NoRecipesProps): JSX.Element => {
    const navigate = useNavigate();

    return (
        <Result
            icon={<FrownOutlined style={{ color: 'rgb(0, 100, 0)' }} />}
            title={pageText || 'No Recipes could be found, please try a different view.'}
            extra={<Button type="primary" onClick={() => navigate('/')}>Back Home</Button>}
        />
    );
};

export default NoRecipes;

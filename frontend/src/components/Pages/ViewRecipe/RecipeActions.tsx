import { Button, Space } from 'antd';
import {
    HeartFilled,
    HeartOutlined,
    EditOutlined,
    PrinterOutlined,
} from '@ant-design/icons';
import { useContext } from 'react';
import AppContext from '@contexts/AppContext';

type RecipeActionsProps = {
    isFavorite: boolean;
    onToggleFavorite: () => void;
    onEdit?: (() => void) | null;
    onPrint?: (() => void) | null;
}

const RecipeActions = ({
    isFavorite,
    onToggleFavorite,
    onEdit = null,
    onPrint = null,
}: RecipeActionsProps): JSX.Element => {
    const { user } = useContext(AppContext);

    return (
        <Space direction="vertical" className="recipe-actions">
            {user ? (
                <Button
                    className="favorite"
                    icon={isFavorite ? <HeartFilled /> : <HeartOutlined />}
                    onClick={onToggleFavorite}
                >
                    {isFavorite ? 'Remove Favorite' : 'Favorite'}
                </Button>
            ) : null}
            {onEdit ? (
                <Button
                    icon={<EditOutlined />}
                    onClick={onEdit}
                >
                    Edit
                </Button>
            ) : null}

            {onPrint ? (
                <Button
                    icon={<PrinterOutlined />}
                    onClick={onPrint}
                >
                    Print
                </Button>
            ) : null}
        </Space>
    );
};

export default RecipeActions;

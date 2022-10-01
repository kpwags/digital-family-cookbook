import { Button, Space } from 'antd';
import {
    HeartFilled,
    HeartOutlined,
    EditOutlined,
    PrinterOutlined,
} from '@ant-design/icons';

type RecipeActionsProps = {
    isFavorite: boolean;
    onToggle: () => void;
    onEdit?: (() => void) | null;
    onPrint?: (() => void) | null;
}

const RecipeActions = ({
    isFavorite,
    onToggle,
    onEdit = null,
    onPrint = null,
}: RecipeActionsProps): JSX.Element => (
    <Space direction="vertical" className="recipe-actions">
        <Button
            className="favorite"
            icon={isFavorite ? <HeartFilled /> : <HeartOutlined />}
            onClick={onToggle}
        >
            {isFavorite ? 'Remove Favorite' : 'Favorite'}
        </Button>
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

export default RecipeActions;

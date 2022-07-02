import { Meat } from '@models/Meat';
import { Space, Tag, Typography } from 'antd';
import getTagColor from '@utils/getTagColor';

const { Title } = Typography;

type MeatsProps = {
    meats: Meat[]
}

const Meats = ({
    meats = [],
}: MeatsProps): JSX.Element => (
    <Space direction="vertical">
        <Title level={3}>Meats</Title>
        <Space direction="horizontal" wrap>
            {meats.map((m, idx) => (
                <Tag color={getTagColor(idx)} key={m.meatId}>{m.name}</Tag>
            ))}
        </Space>
    </Space>
);

export default Meats;

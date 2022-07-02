import { Category } from '@models/Category';
import getTagColor from '@utils/getTagColor';
import { Space, Tag, Typography } from 'antd';

const { Title } = Typography;

type CategoriesProps = {
    categories: Category[]
}

const Categories = ({
    categories = [],
}: CategoriesProps): JSX.Element => (
    <Space direction="vertical">
        <Title level={3}>Categories</Title>
        <Space direction="horizontal" wrap>
            {categories.map((c, idx) => (
                <Tag color={getTagColor(idx)} key={c.categoryId}>{c.name}</Tag>
            ))}
        </Space>
    </Space>
);

export default Categories;

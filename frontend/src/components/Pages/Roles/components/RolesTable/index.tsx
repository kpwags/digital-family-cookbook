import { RoleType } from '@models/RoleType';
import {
    Table,
    Button,
} from 'antd';
import { ColumnsType } from 'antd/lib/table';

type RolesTableProps = {
    roles: RoleType[],
}

const RolesTable = ({
    roles = [],
}: RolesTableProps): JSX.Element => {
    const columns: ColumnsType<RoleType> = [
        {
            key: 'name',
            title: 'Name',
            dataIndex: 'name',
            width: '80%',
        },
        {
            key: 'actions',
            title: 'Actions',
            render: () => (
                <>
                    <Button type="link">Edit</Button>
                    <Button type="link">Delete</Button>
                </>
            ),
        },
    ];

    return (
        <Table
            columns={columns}
            dataSource={roles}
            pagination={false}
        />
    );
};

export { RolesTable };

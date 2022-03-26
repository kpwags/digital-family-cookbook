import { ReactNode } from 'react';
import { Popconfirm } from 'antd';
import { WarningOutlined } from '@ant-design/icons';

type ConfirmDialogProps = {
    text: string
    confirmText?: string
    cancelText?: string
    onConfirm: () => void
    children: ReactNode
}

const ConfirmDialog = ({
    text,
    onConfirm,
    children,
    confirmText = 'Yes',
    cancelText = 'No',
}: ConfirmDialogProps): JSX.Element => (
    <Popconfirm
        okText={confirmText}
        cancelText={cancelText}
        title={text}
        onConfirm={onConfirm}
        icon={<WarningOutlined style={{ color: 'rgb(182, 28, 31)' }} />}
    >
        {children}
    </Popconfirm>
);

export default ConfirmDialog;

import { ReactNode } from 'react';
import {
    Modal,
    ModalProps,
} from 'antd';

export interface FormModalProps extends ModalProps {
    key?: string
    children: ReactNode
}

const FormModal = ({
    key = '',
    visible = false,
    title = '',
    onOk = () => { /* Do nothing by default */ },
    onCancel = () => { /* Do nothing by default */ },
    children,
    okText = 'OK',
    cancelText = 'Cancel',
    okType = 'primary',
    cancelButtonProps = { type: 'ghost' },
}: FormModalProps): JSX.Element => (
    <Modal
        key={key}
        visible={visible}
        title={title}
        onOk={onOk}
        okText={okText}
        okType={okType}
        cancelText={cancelText}
        onCancel={onCancel}
        cancelButtonProps={cancelButtonProps}
    >
        {children}
    </Modal>
);

export { FormModal };

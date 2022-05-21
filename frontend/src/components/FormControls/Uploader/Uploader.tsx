import {
    Form,
    Upload,
    message,
} from 'antd';
import { LoadingOutlined, PlusOutlined } from '@ant-design/icons';
import { Rule } from 'antd/lib/form';
import { useState } from 'react';

type UploaderProps = {
    name: string
    label: string
    inputType?: string
    required?: boolean
    width?: number | string
    rules?: Rule[]
    mode?: 'numeric' | 'string'
    value?: string | number
    extra?: string
}

const Uploader = ({
    name,
    label,
    inputType = 'text',
    required = false,
    rules = [],
    width = '100%',
    mode = 'string',
    value = mode === 'numeric' ? 0 : '',
    extra,
}: UploaderProps): JSX.Element => {
    const [imageUrl, setImageUrl] = useState<string | undefined>(undefined);
    const handleUpload = (info) => {
        console.log({ info });
    };

    return (
        <Form.Item
            name={name}
            label={label}
            rules={rules}
            required={required}
            extra={extra}
            className={extra ? 'has-extra' : ''}
        >
            <Upload
                name={name}
                listType="picture-card"
                className="avatar-uploader"
                showUploadList={false}
                beforeUpload={() => false}
                onChange={handleUpload}
            >
                {imageUrl ? <img src={imageUrl} alt="avatar" style={{ width: '100%' }} /> : uploadButton}
            </Upload>
        </Form.Item>
    );
};

export default Uploader;

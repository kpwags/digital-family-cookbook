import {
    Form,
    Upload,
    Button,
} from 'antd';
import { UploadOutlined } from '@ant-design/icons';
import { Rule } from 'antd/lib/form';
import { useState } from 'react';
import { RcFile, UploadChangeParam } from 'antd/lib/upload';
import { UploadFile } from 'antd/lib/upload/interface';

type UploaderProps = {
    name: string
    label: string
    required?: boolean
    rules?: Rule[]
    extra?: string
    hidden: boolean
    onUpload: (file?: RcFile) => Promise<boolean>
}

const Uploader = ({
    name,
    label,
    required = false,
    rules = [],
    extra,
    onUpload,
    hidden = false,
}: UploaderProps): JSX.Element => {
    const [isUploading, setIsUploading] = useState<boolean>(false);

    const handleUpload = async (info: UploadChangeParam<UploadFile>) => {
        setIsUploading(true);

        await onUpload(info.fileList[0].originFileObj);

        setIsUploading(false);
    };

    return (
        <Form.Item
            name={name}
            label={label}
            rules={rules}
            required={required}
            extra={extra}
            hidden={hidden}
            className={extra ? 'has-extra' : ''}
        >
            <Upload
                name={name}
                showUploadList={false}
                fileList={[]}
                beforeUpload={() => false}
                onChange={handleUpload}
            >
                <Button icon={<UploadOutlined />} disabled={isUploading}>Click to Upload</Button>
            </Upload>
        </Form.Item>
    );
};

export default Uploader;

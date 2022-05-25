import {
    Form,
    Upload,
    message,
} from 'antd';
import { LoadingOutlined, PlusOutlined } from '@ant-design/icons';
import { Rule } from 'antd/lib/form';
import { useState } from 'react';
import { RcFile, UploadChangeParam } from 'antd/lib/upload';
import { UploadFile } from 'antd/lib/upload/interface';
import ImageUploadResponse from '@models/ImageUploadResponse';

type UploaderProps = {
    name: string
    label: string
    required?: boolean
    rules?: Rule[]
    extra?: string
    onUpload: (file?: RcFile) => Promise<{ isSuccessful: boolean, response?: ImageUploadResponse, error?: string }>
}

const Uploader = ({
    name,
    label,
    required = false,
    rules = [],
    extra,
    onUpload,
}: UploaderProps): JSX.Element => {
    const [isUploading, setIsUploading] = useState<boolean>(false);
    const [imageUrl, setImageUrl] = useState<string | undefined>(undefined);

    const handleUpload = async (info: UploadChangeParam<UploadFile>) => {
        setIsUploading(true);

        const { isSuccessful, response, error } = await onUpload(info.fileList[0].originFileObj);

        if (!isSuccessful || !response) {
            message.error(error || 'Error uploading file');
            setImageUrl(undefined);
            setIsUploading(false);
            return;
        }

        setImageUrl(`data:image/png;base64,${response.imageData}`);
        setIsUploading(false);
    };

    const uploadButton = (
        <div>
            {isUploading ? <LoadingOutlined /> : <PlusOutlined />}
            <div style={{ marginTop: 8 }}>Upload</div>
        </div>
    );

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
                fileList={[]}
                beforeUpload={() => false}
                onChange={handleUpload}
            >
                {imageUrl ? <img src={imageUrl} alt="avatar" style={{ width: '100%' }} /> : uploadButton}
            </Upload>
        </Form.Item>
    );
};

export default Uploader;

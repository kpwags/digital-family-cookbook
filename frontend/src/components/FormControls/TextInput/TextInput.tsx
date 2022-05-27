import {
    Form,
    Input,
} from 'antd';
import { Rule } from 'antd/lib/form';

type TextInputProps = {
    name: string
    label: string
    inputType?: string
    required?: boolean
    width?: number | string
    rules?: Rule[]
    value?: string
    extra?: string
}

const TextInput = ({
    name,
    label,
    inputType = 'text',
    required = false,
    rules = [],
    width = '100%',
    value = '',
    extra,
}: TextInputProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
        rules={rules}
        required={required}
        extra={extra}
        className={extra ? 'has-extra' : ''}
    >
        <Input
            type={inputType}
            style={{ width }}
            data-testid={name}
            value={value}
        />
    </Form.Item>
);

export default TextInput;

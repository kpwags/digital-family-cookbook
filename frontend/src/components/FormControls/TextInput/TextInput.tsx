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
    rules?: Rule[]
}

const TextInput = ({
    name,
    label,
    inputType = 'text',
    required = false,
    rules = [],
}: TextInputProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
        rules={rules}
        required={required}
    >
        <Input type={inputType} />
    </Form.Item>
);

export default TextInput;

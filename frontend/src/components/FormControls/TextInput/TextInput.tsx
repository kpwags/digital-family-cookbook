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
    mode?: 'numeric' | 'string'
    value?: string | number
}

const TextInput = ({
    name,
    label,
    inputType = 'text',
    required = false,
    rules = [],
    width = '100%',
    mode = 'string',
    value = mode === 'numeric' ? 0 : '',
}: TextInputProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
        rules={rules}
        required={required}
    >
        <Input
            type={inputType}
            style={{ width }}
            pattern={mode === 'numeric' ? '[0-9]*' : undefined}
            inputMode={mode === 'numeric' ? 'numeric' : undefined}
            value={value}
        />
    </Form.Item>
);

export default TextInput;

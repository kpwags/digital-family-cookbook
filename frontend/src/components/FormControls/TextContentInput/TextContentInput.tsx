import {
    Form,
    Input,
} from 'antd';
import { Rule } from 'antd/lib/form';

type TextContentInputProps = {
    name: string
    label: string
    rows?: number
    required?: boolean
    placeholder?: string
    rules?: Rule[]
}

const { TextArea } = Input;

const TextContentInput = ({
    name,
    label,
    rows = 6,
    required = false,
    rules = [],
    placeholder,
}: TextContentInputProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
        rules={rules}
        required={required}
    >
        <TextArea
            rows={rows}
            placeholder={placeholder}
        />
    </Form.Item>
);

export default TextContentInput;

import {
    Form,
    InputNumber,
} from 'antd';
import { Rule } from 'antd/lib/form';

type NumericInputProps = {
    name: string
    label: string
    inputType?: string
    required?: boolean
    width?: number | string
    rules?: Rule[]
    value?: number
    extra?: string
}

const NumericInput = ({
    name,
    label,
    inputType = 'text',
    required = false,
    rules = [],
    width = '100%',
    value,
    extra,
}: NumericInputProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
        rules={rules}
        required={required}
        extra={extra}
        className={extra ? 'has-extra' : ''}
    >
        <InputNumber
            type={inputType}
            style={{ width }}
            data-testid={name}
            value={value}
        />
    </Form.Item>
);

export default NumericInput;

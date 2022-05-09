import SelectOption from '@models/SelectOption';
import {
    Form,
    Select,
} from 'antd';
import { Rule } from 'antd/lib/form';

const { Option } = Select;

type MultiselectProps = {
    name: string
    label: string
    options: SelectOption[]
    required?: boolean
    rules?: Rule[]
}

const Multiselect = ({
    name,
    label,
    options = [],
    required = false,
    rules = [],
}: MultiselectProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
        rules={rules}
        required={required}
    >
        <Select
            allowClear
            showSearch
            mode="multiple"
        >
            {options.map((o) => (
                <Option key={o.value} value={o.value}>{o.text}</Option>
            ))}
        </Select>
    </Form.Item>
);

export default Multiselect;

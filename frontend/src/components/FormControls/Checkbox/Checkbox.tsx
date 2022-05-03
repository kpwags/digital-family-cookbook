import {
    Form,
    Checkbox as AntCheckbox,
} from 'antd';

type CheckboxProps = {
    name: string
    label: string
    checked?: boolean
}

const Checkbox = ({
    name,
    label,
    checked = false,
}: CheckboxProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
    >
        <AntCheckbox
            checked={checked}
        />
    </Form.Item>
);

export default Checkbox;

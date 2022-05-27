import {
    Form,
    Switch as AntSwitch,
} from 'antd';

type CheckboxProps = {
    name: string
    label: string
    checked?: boolean
    onChange: (isChecked: boolean) => void
}

const Switch = ({
    name,
    label,
    checked = false,
    onChange,
}: CheckboxProps): JSX.Element => (
    <Form.Item
        name={name}
        label={label}
    >
        <AntSwitch
            checked={checked}
            onChange={onChange}
        />
    </Form.Item>
);

export default Switch;

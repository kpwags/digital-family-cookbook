import {
    Form,
} from 'antd';
import { Rule } from 'antd/lib/form';
import QuillEditor from './QuillEditor';

import './HtmlEditor.less';

type HtmlEditorProps = {
    name: string
    placeholder?: string
    label: string
    required?: boolean
    rules?: Rule[]
}

const HtmlEditor = ({
    name,
    placeholder,
    label,
    required,
    rules,
}: HtmlEditorProps): JSX.Element => (
    <Form.Item
        className="quilleditor"
        name={name}
        label={label}
        rules={rules}
        required={required}
    >
        <QuillEditor
            name={name}
            placeholder={placeholder}
        />
    </Form.Item>
);

export default HtmlEditor;

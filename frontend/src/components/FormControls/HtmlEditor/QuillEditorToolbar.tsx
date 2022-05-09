/* eslint-disable jsx-a11y/control-has-associated-label */
/* eslint-disable react/button-has-type */
import { SnippetsOutlined } from '@ant-design/icons';

const QuillEditorToolbar = ({ id }: { id: string }): JSX.Element => (
    <div id={`toolbar_${id}`}>
        <select className="ql-size" defaultValue="medium">
            <option value="extra-small">Extra Small</option>
            <option value="small">Small</option>
            <option value="medium">Normal</option>
            <option value="large">Large</option>
        </select>
        <button className="ql-bold" />
        <button className="ql-italic" />
        <button className="ql-underline" />
        <button className="ql-strike" />
        <select className="ql-align" />
        <button className="ql-list" value="bullet" />
        <button className="ql-link" />
        <button className="ql-clean" />
        <button className="ql-pasteFormatting">
            <SnippetsOutlined />
        </button>
    </div>
);

export default QuillEditorToolbar;

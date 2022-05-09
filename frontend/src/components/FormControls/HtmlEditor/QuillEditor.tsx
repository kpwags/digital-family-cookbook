import pasteFormatting from '@utils/pasteFormatting';
import ReactQuill, { Quill } from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import QuillEditorToolbar from './QuillEditorToolbar';

type QuillEditorProps = {
    name: string
    value?: string
    testId?: string
    placeholder?: string
    onChange?: (val: string) => void
}

const QuillEditor = ({
    name,
    value,
    testId,
    placeholder,
    onChange,
}: QuillEditorProps): JSX.Element => {
    const modules = {
        toolbar: {
            container: `#toolbar_${name}`,
            handlers: { pasteFormatting },
        },
    };

    const Size = Quill.import('formats/size');
    Size.whitelist = ['extra-small', 'small', 'medium', 'large'];
    Quill.register(Size, true);

    const formats = [
        'header',
        'font',
        'size',
        'bold',
        'italic',
        'underline',
        'strike',
        'list',
        'link',
        'align',
    ];

    return (
        <>
            <QuillEditorToolbar id={name} />
            <ReactQuill
                theme="snow"
                className="quilleditor__input"
                value={value || ''}
                data-testid={testId || ''}
                modules={modules}
                onChange={onChange}
                formats={formats}
                placeholder={placeholder}
            />
        </>
    );
};

export const validateEmptyMarkup = (_: string, value: string): Promise<void> => {
    if (value === '<p><br></p>') {
        return Promise.reject(new Error('This field is required.'));
    }

    return Promise.resolve();
};

export default QuillEditor;

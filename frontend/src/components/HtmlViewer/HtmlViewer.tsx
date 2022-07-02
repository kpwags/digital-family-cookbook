/* eslint-disable react/no-danger */
type HtmlViewerProps = {
    html: string
}

const HtmlViewer = ({
    html,
}: HtmlViewerProps): JSX.Element => (
    <div dangerouslySetInnerHTML={{ __html: html }} />
);

export default HtmlViewer;

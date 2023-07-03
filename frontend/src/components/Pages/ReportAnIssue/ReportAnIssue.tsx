import { Button, Space, Typography } from 'antd';

import './ReportAnIssue.less';

const { Link, Title, Paragraph } = Typography;

const ReportAnIssue = (): JSX.Element => (
    <div className="page-container report-an-issue">
        <Title level={1}>Report An Issue</Title>
        <Paragraph>As embarassing as it might be, this software is not perfect and there could of course be bugs and other less-than-favorable issues.</Paragraph>
        <Paragraph>
            If you run into a bug or other issue, please visit our <Link href="https://github.com/kpwags/digital-family-cookbook">GitHub Repository</Link> and
            create an issue for it and I can look into it and hopefully quickly fix it.
        </Paragraph>
        <Paragraph>Also, feel free to create an issue if you see something that could be improved or if you have a feature you&apos;d like to request.</Paragraph>
        <Space direction="horizontal" size={12} className="button-area">
            <Button
                type="primary"
                href="https://github.com/kpwags/digital-family-cookbook/issues/new?assignees=&labels=&projects=&template=bug_report.md&title="
            >
                Report a Bug
            </Button>
            <Button
                type="primary"
                href="https://github.com/kpwags/digital-family-cookbook/issues/new?assignees=&labels=&projects=&template=feature_request.md&title="
            >
                Request a Feature
            </Button>
        </Space>
    </div>
);

export default ReportAnIssue;

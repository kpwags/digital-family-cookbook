import { ReactNode } from 'react';
import { Layout } from 'antd';
import NavBar from '@components/Elements/NavBar';

const { Content } = Layout;

type BaseLayoutProps = {
    children: ReactNode
}

const BaseLayout = ({
    children,
}: BaseLayoutProps): JSX.Element => (
    <Layout>
        <NavBar />
        <Content className="site-content">
            {children}
        </Content>
    </Layout>
);

export default BaseLayout;

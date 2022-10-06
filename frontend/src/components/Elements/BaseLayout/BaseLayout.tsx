import { ReactNode } from 'react';
import { Layout } from 'antd';
import NavBar from '@components/Elements/NavBar';
import Footer from '@components/Elements/Footer';

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
        <Footer />
    </Layout>
);

export default BaseLayout;

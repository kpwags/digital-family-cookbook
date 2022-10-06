import { useContext } from 'react';
import AppContext from '@contexts/AppContext';
import { Link } from 'react-router-dom';
import { Layout } from 'antd';

import './Footer.less';

const Footer = (): JSX.Element => {
    const { siteSettings, user } = useContext(AppContext);

    return (
        <Layout.Footer className="footer">
            <ul>
                <li><Link to="/">Home</Link></li>
                {siteSettings.isPublic || user !== null ? (<li><Link to="/search">Search</Link></li>) : null}
                {user !== null ? (<li><Link to="/recipes/add">Add Recipe</Link></li>) : null}
            </ul>
            <div className="copyright">&copy; {(new Date()).getFullYear()} {siteSettings.title}</div>
        </Layout.Footer>
    );
};

export default Footer;

import { useContext } from 'react';
import {
    Typography,
    Button,
} from 'antd';
import { useNavigate } from 'react-router-dom';
import AppContext from '@contexts/AppContext';
import { EMPTY_TEXT_VALUE } from '@components/FormControls/HtmlEditor/QuillEditor';

import './PrivateLanding.less';
import HtmlViewer from '@components/HtmlViewer';

const { Title } = Typography;

const PrivateLanding = (): JSX.Element => {
    const { siteSettings } = useContext(AppContext);

    const navigate = useNavigate();

    return (
        <div className="private-landing">
            <Title level={1}>Welcome to {siteSettings.title}</Title>

            {siteSettings.landingPageText !== '' && siteSettings.landingPageText !== EMPTY_TEXT_VALUE ? (
                <HtmlViewer html={siteSettings.landingPageText || ''} />
            ) : null}

            <div className="action-items">
                <Button
                    type="primary"
                    onClick={() => navigate('/register')}
                >
                    Register for an Account
                </Button>
            </div>
        </div>
    );
};

export default PrivateLanding;

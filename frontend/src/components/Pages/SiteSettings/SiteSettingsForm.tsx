import { useState, useContext } from 'react';
import {
    Spin,
    Alert,
    Form,
    Input,
    Checkbox,
    Button,
    Typography,
    message,
} from 'antd';
import AppContext from '@contexts/AppContext';
import { Api } from '@utils/api';
import { SiteSettings } from '@models/SiteSettings';
import HtmlEditor from '@components/FormControls/HtmlEditor';

const { Title } = Typography;

type FormValues = {
    title: string
    landingPageText: string
    isPublic: boolean
    allowPublicRegistration: boolean
}

const SiteSettingsForm = (): JSX.Element => {
    const [loadingMessage, setLoadingMessage] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string>('');

    const [form] = Form.useForm<FormValues>();

    const { siteSettings, updateSiteSettings } = useContext(AppContext);

    const submitForm = async ({
        title,
        landingPageText,
        isPublic,
        allowPublicRegistration,
    }: FormValues) => {
        setLoadingMessage('Saving...');
        setErrorMessage('');

        const [, error] = await Api.Post('system/savesitesettings', {
            data: {
                title,
                isPublic,
                landingPageText,
                allowPublicRegistration,
            },
        });

        if (error) {
            setErrorMessage(error);
            setLoadingMessage('');
            return;
        }

        updateSiteSettings({
            id: siteSettings.id,
            siteSettingsId: siteSettings.siteSettingsId,
            title,
            landingPageText,
            isPublic,
            allowPublicRegistration,
            invitationCode: siteSettings.invitationCode,
        });

        setLoadingMessage('');
        message.success('Settings saved successfully');
    };

    const refreshInvitationCode = async () => {
        setLoadingMessage('Refreshing...');
        setErrorMessage('');

        const [data, error] = await Api.Post<SiteSettings>(
            'system/refreshinvitationcode',
            {
                data: {},
            },
        );

        if (error) {
            setErrorMessage(error);
            setLoadingMessage('');
            return;
        }

        updateSiteSettings(data || siteSettings);
        setLoadingMessage('');
    };

    return (
        <Spin
            size="large"
            spinning={loadingMessage !== ''}
            tip={loadingMessage}
        >
            {errorMessage !== '' ? (
                <Alert
                    type="error"
                    message={errorMessage}
                />
            ) : null}

            <Form
                className="site-settings-form"
                form={form}
                labelAlign="right"
                initialValues={{
                    title: siteSettings.title,
                    isPublic: siteSettings.isPublic,
                    allowPublicRegistration: siteSettings.allowPublicRegistration,
                    landingPageText: siteSettings.landingPageText,
                }}
                onFinish={(values: FormValues) => {
                    submitForm(values);
                }}
            >
                <Form.Item
                    name="title"
                    label="Title"
                    rules={[
                        { required: true, message: 'Title is required' },
                    ]}
                    required
                >
                    <Input
                        type="text"
                    />
                </Form.Item>

                <Form.Item
                    name="isPublic"
                    valuePropName="checked"
                >
                    <Checkbox>Is Public</Checkbox>
                </Form.Item>

                <HtmlEditor
                    name="landingPageText"
                    label="Landing Page Text"
                />

                <Form.Item
                    name="allowPublicRegistration"
                    valuePropName="checked"
                >
                    <Checkbox>Allow Public Registration</Checkbox>
                </Form.Item>

                <div className="token-section">
                    <Title level={3}>Invitation Code</Title>

                    <p>{siteSettings.invitationCode}</p>

                    <Button
                        type="ghost"
                        onClick={() => refreshInvitationCode()}
                    >
                        Refresh Code
                    </Button>
                </div>

                <Form.Item
                    className="action-area"
                >
                    <Button
                        type="primary"
                        htmlType="submit"
                    >
                        Save
                    </Button>
                </Form.Item>
            </Form>
        </Spin>
    );
};

export default SiteSettingsForm;

import AppContext from '@contexts/AppContext';
import { useEffect, useContext } from 'react';

const useDocumentTitle = (title = ''): void => {
    const { siteSettings } = useContext(AppContext);

    useEffect(() => {
        document.title = title === '' ? siteSettings.title : `${title} - ${siteSettings.title}`;
    }, [title]);
};

export default useDocumentTitle;

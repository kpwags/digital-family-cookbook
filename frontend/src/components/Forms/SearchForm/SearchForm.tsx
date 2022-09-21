import { useState, useEffect } from 'react';
import {
    Spin,
    Input,
    Typography,
} from 'antd';

import './SearchForm.less';

const { Search } = Input;
const { Text } = Typography;

type SearchFormProps = {
    keywords?: string;
    processingMessage: string;
    onSearch: (keywords: string) => void;
}

const SearchForm = ({
    onSearch,
    keywords = '',
    processingMessage = '',
}: SearchFormProps): JSX.Element => {
    const [searchTerms, setSearchTerms] = useState<string>(keywords);
    const [searchError, setSearchError] = useState<string>('');

    useEffect(() => {
        setSearchTerms(keywords);
    }, [keywords]);

    return (
        <div className="search-form-container">
            <Spin
                size="large"
                spinning={processingMessage !== ''}
                tip={processingMessage}
            >
                <div className="search-form">
                    <Search
                        data-testid="search-keywords"
                        placeholder="Enter Search Terms"
                        name="keywords"
                        onChange={(e) => setSearchTerms(e.target.value)}
                        value={searchTerms}
                        onSearch={(val) => {
                            if (val.trim().length === 0) {
                                setSearchError('Please enter search keywords');
                                return;
                            }

                            setSearchError('');
                            onSearch(val);
                        }}
                        enterButton="Search"
                    />
                </div>
                {searchError !== '' ? (
                    <div className="search-error">
                        <Text type="danger">{searchError}</Text>
                    </div>
                ) : null}
            </Spin>
        </div>
    );
};

export default SearchForm;

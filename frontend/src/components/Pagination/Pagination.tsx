import { Pagination as AntPagination } from 'antd';

import './Pagination.less';

type PaginationProps = {
    recipeCount: number
    recipesPerPage: number
    currentPageNumber: number
    onChange: (pageNumber: number, pageSize: number) => void
}

const Pagination = ({
    recipeCount,
    onChange,
    recipesPerPage = 10,
    currentPageNumber = 1,
}: PaginationProps): JSX.Element => (
    <AntPagination
        className="pagination"
        total={recipeCount}
        showTotal={(total, range) => `${range[0]}-${range[1]} of ${total} recipes`}
        defaultPageSize={4}
        pageSize={recipesPerPage}
        current={currentPageNumber}
        defaultCurrent={1}
        showSizeChanger
        onChange={onChange}
    />
);

export default Pagination;

const setPageUrl = (url: string): void => {
    window.history.pushState({}, '', url);
};

export default setPageUrl;

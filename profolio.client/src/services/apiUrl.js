export const apiUrl = {
    login: `/api/Login`,
    article: {
        detail: (title = '') => `/api/Article/${title}`, //函數(參數) => 路徑)
        cards: `/api/Article/Cards`,
        sync: `/api/Article/Sync`,
        subTitle: `/api/Article/SubTitle`,
        blog: `/api/Article/Blogs`,
        search: (query = '') => `/api/Article/Search?query=${encodeURIComponent(query)}`,
    },
    tag: {
        tagTree: `/api/Tag/TagTree`,
    },
    profile: {
        overview: `/api/Profile/Overview`,
        updateTimeline: `/api/Profile/Timeline`,
    },
    errorLog: `/api/ErrorLog/Log`,
};
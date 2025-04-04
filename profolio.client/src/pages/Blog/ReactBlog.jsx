import './ReactBlog.css'
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { get } from '../../services/api';
import { apiUrl } from '../../services/apiUrl';
import MarkdownRender from '../../components/MarkdownRender.jsx';

const Blog = () => {
    const [posts, setPosts] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [page, setPage] = useState(1);
    const [totalCount, setTotalCount] = useState(0); // 文章總數

    const fetchMorePosts = async (controller) => {
        if (isLoading) return;
        setIsLoading(true);

        try {
            const response = await get(`${apiUrl.article.blog}?page=${page}&pageSize=3`, {}, controller.signal);
            if (!response || !response.data) return;
            setPosts((prevPosts) => [...prevPosts, ...response.data]);
            if (response.totalCount !== undefined) setTotalCount(response.totalCount);
            setPage((prevPage) => prevPage + 1);
        } catch (error) {
            if (error.name === "AbortError") {
                console.log("Fetch aborted!");
            } else {
                console.error("Error fetching posts:", error);
            }
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        const controller = new AbortController(); // 建立 `AbortController`
        fetchMorePosts(controller);

        return () => {
            controller.abort(); // 確保組件卸載時取消請求
        };
    }, []);


    return (
        <div className="blog_container">
            <div className="blog_title">
                <h1>React Blog</h1>
                <p>This blog is the official source for updates from the React team.</p>
            </div>
            <div className="blog-list">
                {posts.map((post) => (
                    <div key={post.title} className="blog-card">
                        <h2 className="title">{post.title}</h2>
                        <p className="date">{post.updateAtString ? post.updateAtString : post.createdAtString}</p>
                        <p className="content"><MarkdownRender mdText={post.content} /></p>
                        <Link to={`/reactArticle/${post.id}`} className="readMore">Read more</Link>
                    </div>
                ))}
            </div>
            {/* 只有當 `posts.length < totalCount` 時才顯示 `See More` 按鈕 */}
            {posts.length < totalCount && (
                <div className="load-more-container">
                    <button onClick={fetchMorePosts} className="load-more-btn">
                        {isLoading ? "Loading..." : "See More"}
                    </button>
                </div>
            )}
        </div>
    );
};

export default Blog;

import './ReactArticle.css';
import { useEffect, useState } from "react";
import { useNavigate, useParams, Link, useLocation } from "react-router-dom";
import { get } from '../../services/api.js';
import { apiUrl } from '../../services/apiUrl.js';
import Box from '@mui/material/Box';
import { RichTreeView } from '@mui/x-tree-view/RichTreeView';
import { useTreeViewApiRef } from '@mui/x-tree-view/hooks/useTreeViewApiRef';
import MarkdownRender from '../../components/MarkdownRender.jsx';
import { HiChevronDoubleLeft } from "react-icons/hi";
import { HiChevronDoubleRight } from "react-icons/hi";

const Article = () => {
    const { id } = useParams();
    const [article, setArticle] = useState(null);
    const [categories, setCategories] = useState([]);
    const [toc, setToc] = useState([]);
    const [prevArticle, setPrevArticle] = useState(null);
    const [nextArticle, setNextArticle] = useState(null);
    const navigate = useNavigate();
    const apiRef = useTreeViewApiRef();

    const flattenArticles = (nodes) =>
        nodes.reduce((acc, node) => {
            return node.children?.length ? acc.concat(flattenArticles(node.children)) : acc.concat(node)
        }, []);

    const handleItemClick = (event, itemId) => {
        const node = findNodeById(categories, itemId);
        if (!node) return;
        const isArticle = !node.children?.length;
        if (isArticle) {
            navigate(`/reactArticle/${node.id}`);
        }
    };

    const findNodeById = (nodes, targetId) => {
        for (const node of nodes) {
            if (node.id === targetId) return node;
            if (node.children) {
                const found = findNodeById(node.children, targetId);
                if (found) return found;
            }
        }
        return null;
    };
    const headingRegex = /^(#+)\s+(.*)/gm;
    useEffect(() => {
        const fetchData = async () => {
            try {
                const categRes = await get(apiUrl.tag.tagTree);
                const articleRes = await get(apiUrl.article.detail(id));
                setCategories(categRes);
                setArticle(articleRes);
                // 解析 Markdown 產生 TOC
                let headingMatch = [...articleRes.content.matchAll(headingRegex)];
                setToc(headingMatch && headingMatch.map(match => ({
                    text: match[2],
                    id: match[2].toLowerCase().replace(/\s+/g, "-"),
                    level: match[1].length,
                })));

                if (categRes && categRes.length > 0) {
                    const sortedArticles = flattenArticles(categories);
                    const currentIndex = sortedArticles.findIndex(item => item.id === id);
                    setPrevArticle(currentIndex > 0 ? sortedArticles[currentIndex - 1] : null); //broke
                    setNextArticle(currentIndex < sortedArticles.length - 1 ? sortedArticles[currentIndex + 1] : null); //broke
                }
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, [id]);

    return (
        <section className="article-section">
            <div className="article-page">
                <aside className="sidebar">
                    <h2>Articles</h2>
                    <Box sx={{ minHeight: 352, minWidth: 200 }}>
                        <RichTreeView apiRef={apiRef} items={categories} onItemClick={handleItemClick}
                        />
                    </Box>
                </aside>

                <main className="content_section">
                    <div className="article">
                        {article && (<div>
                            <h1 className="article-title">{article.title}</h1>
                            <hr />
                            <div className="article-content">
                                <MarkdownRender mdText={article.content} />
                            </div>
                            <p className="publish-time">Published at: {article.createdAtString}</p>
                        </div>
                        )}
                    </div>

                    <div className="pagination">
                        {prevArticle && <Link to={`/reactArticle/${prevArticle.id}`} className="prev"><HiChevronDoubleLeft />{prevArticle.label}</Link>}
                        {nextArticle && <Link to={`/reactArticle/${nextArticle.id}`} className="next">{nextArticle.label}<HiChevronDoubleRight /></Link>}
                    </div>
                </main>

                <aside className="toc">
                    <h3>Table Of Contents</h3>
                    <ul className="table_contents">
                        {toc && toc.map((heading) => (
                            <li key={heading.id} style={{ marginLeft: (heading.level - 2) * 10 + "px" }}>
                                <a href={`#${heading.id}`}>{heading.text}</a>
                            </li>
                        ))}
                    </ul>
                </aside>
            </div>
        </section>
    );
};

export default Article;
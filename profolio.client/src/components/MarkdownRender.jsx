import { useEffect, useState } from "react";
import MarkdownIt from "markdown-it";
import markdownItAnchor from "markdown-it-anchor";
import markdownItTocDoneRight from "markdown-it-toc-done-right";
import hljs from "highlight.js";
import markdownItContainer from "markdown-it-container";
import "highlight.js/styles/github.css";
import "./MarkdownRender.css";

const MarkdownRender = ({ mdText }) => {
    const [html, setHtml] = useState("");

    useEffect(() => {
        const md = new MarkdownIt({
            html: true,
            linkify: true,
            typographer: true,
            highlight: function (str, lang) {
                if (lang) {
                    lang = lang.replace(/!$/, "");
                    if (hljs.getLanguage(lang)) {
                        try {
                            const highlighted = hljs.highlight(str, {
                                language: lang,
                                ignoreIllegals: true,
                            }).value;
                            return `<pre><code class="hljs language-${lang}">${highlighted}</code></pre>`;
                        } catch (error) { console.error(error); }
                    }
                }
                const escaped = md.utils.escapeHtml(str);
                return `<pre><code class="hljs">${escaped}</code></pre>`;
            },
        });

        const containerTypes = ["info", "warning", "success", "danger"];
        containerTypes.forEach((type) => {
            md.use(markdownItContainer, type, {
                validate: function (params) {
                    return params.trim().match(new RegExp(`^${type}(\\s+.*)?$`));
                },
                render: function (tokens, idx) {
                    const m = tokens[idx].info.trim().match(new RegExp(`^${type}(\\s+(.*))?$`));
                    if (tokens[idx].nesting === 1) {
                        const title = m && m[2] ? m[2] : type.toUpperCase();
                        return `<details class="alert-box ${type}">\n<summary class="container-title">${md.utils.escapeHtml(title)}</summary>\n`;
                    } else {
                        return "</details>\n";
                    }
                },
            });
        });

        md.use(markdownItAnchor).use(markdownItTocDoneRight);
        setHtml(md.render(mdText));
    }, [mdText]);

    return <div dangerouslySetInnerHTML={{ __html: html }} />;
};

export default MarkdownRender;

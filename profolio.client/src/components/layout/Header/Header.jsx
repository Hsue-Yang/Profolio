import './Header.css'
import Nav from "../Nav/Nav.jsx";
import { Link } from 'react-router-dom';
import ThemeToggle from '../Nav/ThemeToggle.jsx'
import * as Icons from "react-icons/fa";
import { IconContext } from "react-icons";
import { useRef, useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { get } from '../../../services/api'
import { apiUrl } from '../../../services/apiUrl.js'

function Header() {
    const [searchTerm, setSearchTerm] = useState('');
    const [searchResults, setSearchResults] = useState([]);
    const [showDropdown, setShowDropdown] = useState(false);
    const containerRef = useRef();
    const location = useLocation();
    const iconLinks = [
        { label: 'GitHub', href: 'https://github.com/Hsue-Yang/AstroWeb', icon: 'FaGithub' },
        { label: 'LinkedIn', href: 'https://linkedin.com/in/michael-yang-0ab967238', icon: 'FaLinkedin' },
        { label: 'Instagram', href: 'https://www.instagram.com/mic_8412/', icon: 'FaInstagram' },
        { label: 'HackMD', href: 'https://hackmd.io/@e8-c0t-kR_yMxNoaitut2A', icon: 'FaCodepen' },
    ];

    const handleKeyDown = async (e) => {
        if (e.key === 'Enter') {
            e.preventDefault();
            await performSearch();
        }
    };

    // Called on search icon click
    const handleIconClick = async (e) => {
        e.preventDefault();
        await performSearch();
    };

    // The actual function that calls your API
    const performSearch = async () => {
        if (searchTerm.trim() !== '') {
            try {
                const results = await get(apiUrl.article.search(searchTerm.trim()));
                setSearchResults(results);
                setShowDropdown(true);
            } catch (error) {
                console.error('Search error:', error);
                setSearchResults([]);
                setShowDropdown(true);
            }
        } else {
            setSearchResults([]);
            setShowDropdown(true);
        }
    };

    // Close dropdown if click outside
    useEffect(() => {
        function handleClickOutside(e) {
            if (containerRef.current && !containerRef.current.contains(e.target)) {
                setShowDropdown(false);
            }
        }
        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    useEffect(() => {
        setSearchTerm('');
        setShowDropdown(false);
    }, [location])

    return (
        <header className="header">
            <div className="header-left">
                <Link to="/" className="logo-link">
                    <Icons.FaHome className="logo-icon" size={32} />
                </Link>
            </div>
            <div className="header-search" ref={containerRef}>
                <form className="search-form" onSubmit={(e) => e.preventDefault()}>
                    <input
                        type="text"
                        placeholder="Search..."
                        aria-label="Search"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        onKeyDown={handleKeyDown}
                        onFocus={() => {
                            if (searchResults.length > 0) {
                                setShowDropdown(true);
                            }
                        }}
                    />
                    <button
                        type="submit"
                        className="search-button"
                        onClick={handleIconClick}
                    >
                        <Icons.FaSearch />
                    </button>
                </form>
                {showDropdown && (
                    <div className="search-dropdown">
                        {searchResults?.totalCount === 0 && searchTerm.trim() !== '' ? (
                            <div className="search-item no-result">No search result</div>
                        ) : (searchResults?.searchArticles?.length > 0 &&
                            searchResults.searchArticles.map((article) => (
                                <div key={article.title} className="search-item">
                                    <Link
                                        to={`/reactArticle/${article.title}`}
                                        onClick={() => { setShowDropdown(false); setSearchTerm('') }}
                                    >
                                        {article.title}
                                    </Link>
                                </div>
                            ))
                        )}
                    </div>
                )}
            </div>
            <div className="header-right">
                {/*<Sidebar/>*/}
                <Nav />
                <div className="socials">
                    <IconContext.Provider value={{ size: "1.5em", className: "social-icon" }}>
                        {iconLinks.map(({ href, icon, label }) => {
                            const IconComponent = Icons[icon];
                            if (!IconComponent) return null;
                            return (
                                <a href={href} className="social" key={label} target="_blank" rel="noopener noreferrer" title={label}>
                                    <IconComponent />
                                </a>
                            );
                        })}
                    </IconContext.Provider>
                </div>
                <ThemeToggle />
            </div>
        </header>
    );
}

export default Header;
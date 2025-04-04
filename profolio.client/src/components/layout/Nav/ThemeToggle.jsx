import './ThemeToggle.css'
import { useState, useEffect } from 'react';
import { FaMoon, FaSun } from 'react-icons/fa';

const ThemeToggle = () => {
    const [isDark, setIsDark] = useState(() => {
        return document.documentElement.classList.contains('theme-dark');
    });

    useEffect(() => {
        document.documentElement.classList.toggle('theme-dark', isDark);
    }, [isDark])

    const handleToggle = () => {
        setIsDark((prev) => !prev);
    };

    return (
        <button onClick={handleToggle} aria-pressed={isDark} className={`theme-toggle-button ${isDark ? 'dark' : 'light'}`}>
            <div className="toggle-slider">
                <span className="icon light-icon">
                    <FaSun />
                </span>
                <span className="icon dark-icon">
                    <FaMoon />
                </span>
            </div>
        </button>
    );
}

export default ThemeToggle;
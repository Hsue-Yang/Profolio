import { Link, useLocation } from 'react-router-dom';

const styles = {
    link: {
        textDecoration: 'none',
        color: 'inherit',
    },
    active: {
        fontWeight: 'bold',
    },
};

const NavDesktop = ({ menuItems }) => {
    const location = useLocation();

    return (
        <ul className="nav-list">
            {menuItems.map(({ path, label }) => (
                <li className={`nav-item ${location.pathname === path ? 'active' : ''
                    }`} key={path}>
                    <Link
                        to={path}
                        style={
                            location.pathname === path
                                ? { ...styles.link, ...styles.active }
                                : styles.link
                        }
                    >
                        {label}
                    </Link>
                </li>
            ))}
        </ul>
    );
};

export default NavDesktop;
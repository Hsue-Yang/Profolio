import './Nav.css'
import NavDesktop from './NavDesktop.jsx';

function Nav() {

    return (
        <>
            <nav className="navbar">
                <NavDesktop menuItems={menuItems} />
            </nav>
        </>
    );
}

const menuItems = [
    { path: '/', label: 'Home' },
    { path: '/reactBlog', label: 'Blog' },
];

export default Nav;
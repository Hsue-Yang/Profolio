import './Footer.css'
import ContactForm from '../../Home/ContactForm.jsx';

function Footer() {
    return (
        <footer>
            <ContactForm />
            <div className="copyright">
                <small>
                    Copyright © 2025 Michael Yang's Blog.
                </small>
            </div>
        </footer>
    );
}

export default Footer;
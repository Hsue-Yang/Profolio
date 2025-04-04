import React from "react";
import "tailwindcss";
import './ContactForm.css'

const ContactForm = () => {
    return (
        <section className="contact_section">
            {/* 標題區塊 */}
            <div className="contact_title">
                <h2>Get In Touch</h2>
                <p>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                </p>
            </div>

            {/* 表單區塊 */}
            <form className="contact_form">
                {/* Phone */}
                <div>
                    <label>Phone</label>
                    <input type="text" placeholder="Phone" />
                </div>

                {/* Email */}
                <div>
                    <label>Email</label>
                    <input type="email" placeholder="Email" />
                </div>

                {/* Message */}
                <div>
                    <label>Message</label>
                    <textarea placeholder="Message" rows="4"></textarea>
                </div>

                <div className="submit-container">
                    <button>Submit</button>
                </div>
            </form>
        </section>
    );
};

export default ContactForm;
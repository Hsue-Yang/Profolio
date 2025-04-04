import './Home.css'
import { useEffect, useState } from 'react'
import { get } from '../../services/api.js'
import { apiUrl } from '../../services/apiUrl.js'
import BlogCards from '../../components/Home/BlogCard.jsx';
import Timeline from '../../components/Profile/Timeline.jsx';

const Home = () => {
    const [cards, setCards] = useState([]);
    const [timeline, setTimelines] = useState([]);
    useEffect(() => {
        const fetchInfo = async () => {
            try {
                const timelineRes = await get(apiUrl.profile.overview);
                const cardsRes = await get(apiUrl.article.cards);
                setTimelines(timelineRes.timelines);
                setCards(cardsRes);
            } catch (err) {
                console.error("Error fetching article:", err);
                setTimelines([]);
                setCards([]);
            }
        };
        fetchInfo();
    }, []);

    return (
        <div className="Home_container">
            <section className="subscribe_section">
                <div className="text-section">
                    <h1>Michael Yang</h1>
                    <p>
                        Welcome to my portfolio! Discover creative solutions and innovative designs.
                        Subscribe to stay updated.
                    </p>
                    <div className="subscribe">
                        <input type="email" placeholder="Enter your email" className="subscribe-input" />
                        <button className="subscribe-btn">Subscribe</button>
                    </div>
                    <div className="subscribe_num">
                        <div>
                            <h3>95%</h3>
                            <p>Satisfaction Rate</p>
                        </div>
                        <div>
                            <h3>300K</h3>
                            <p>Projects Completed</p>
                        </div>
                    </div>
                </div>

                {/* ¥k°¼¹Ï¤ù°Ï°ì */}
                <div className="image">
                    <div className="image-wrapper">
                        <img src="mountain.jpg" alt="Portfolio Design" />
                    </div>
                </div>
            </section>
            <section className="blog_section">
                <BlogCards cards={cards} />
            </section>
            <section className="timeline_section">
                <Timeline events={timeline} />
            </section>
        </div>
    );
};

export default Home;
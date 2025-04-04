import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home/Home.jsx';
import ReactBlog from './pages/Blog/ReactBlog.jsx';
import Header from './components/layout/Header/Header.jsx';
import ReactArticle from './pages/Article/ReactArticle.jsx';

function App() {
    return (
        <Router>
            <div className="wrapper">
                <Header />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/reactArticle/:id" element={<ReactArticle />} />
                    <Route path="/reactBlog" element={<ReactBlog />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
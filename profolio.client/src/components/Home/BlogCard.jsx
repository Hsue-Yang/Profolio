import "swiper/css";
import "swiper/css/pagination";
import { Swiper, SwiperSlide } from "swiper/react";
import { Link } from "react-router-dom";
import SwiperCore from "swiper"; // Swiper 核心模組
import { Pagination } from "swiper/modules"; // Swiper 分頁模組
import Card from "@mui/material/Card";
import CardMedia from "@mui/material/CardMedia";
import Avatar from "@mui/material/Avatar";
import Typography from "@mui/material/Typography";
import { Box, Divider } from "@mui/material";
import { deepPurple } from "@mui/material/colors";
import MarkdownRender from '../MarkdownRender.jsx';

// 啟用 Swiper 分頁

const HorizontalSwiperCards = ({ cards }) => {
    SwiperCore.use([Pagination]);
    return (
        <Swiper
            slidesPerView={3} // 一次顯示 3 張卡片
            spaceBetween={30} // 卡片間距
            pagination={{ clickable: true }} // 啟用分頁功能
            breakpoints={{
                768: { slidesPerView: 2 }, // 平板顯示 2 張
                1024: { slidesPerView: 3 }, // 桌機顯示 3 張
            }}
            style={{
                width: "100%",
                maxWidth: "1200px",
                margin: "0 auto",
                paddingBottom: "40px",
            }}
        >
            {cards.map((card, index) => (
                <SwiperSlide key={index}>
                    <Link to={`/reactArticle/${card.id}`} style={{ textDecoration: "none" }}>
                        <Card
                            sx={{
                                display: "flex",
                                flexDirection: "column",
                                alignItems: "center",
                                borderRadius: "20px",
                                overflow: "hidden",
                                transition: "transform 0.3s ease-in-out",
                                "&:hover": { transform: "scale(1.03)" },
                            }}
                        >
                            {/* 上半部圖片區域 */}
                            <Box sx={{ position: "relative", width: "100%" }}>
                                <CardMedia
                                    component="img"
                                    image="mountain.jpg"
                                    alt={card.title}
                                    sx={{ height: "180px", objectFit: "cover" }}
                                />
                                {/* 標籤 */}
                                <Box
                                    sx={{
                                        position: "absolute",
                                        bottom: "-12px", // 讓標籤剛好壓在圖片邊緣
                                        left: "2%",
                                        backgroundColor: "#F2F2F1",
                                        color: "black",
                                        fontWeight: "bold",
                                        padding: "5px 12px",
                                        borderRadius: "20px",
                                        fontSize: "12px",
                                        textAlign: "center",
                                    }}
                                >
                                    {card.tags && card.tags.map((tag) => { tag })} {/*之後改成tag*/}
                                </Box>
                            </Box>

                            {/* 下半部內容區域 */}
                            <Box
                                sx={{
                                    backgroundColor: "#E7E7E8",
                                    width: "100%",
                                    padding: "20px",
                                    display: "flex",
                                    flexDirection: "column",
                                    justifyContent: "space-between",
                                    minHeight: "220px", // 設定統一的最小高度
                                }}
                            >
                                {/* 標題 */}
                                <Typography
                                    variant="h6"
                                    sx={{
                                        fontWeight: "bold",
                                        color: "black",
                                        marginLeft: "10px",
                                        lineHeight: "1.4",
                                    }}
                                >
                                    {card.title}
                                </Typography>

                                {/* 內容描述 */}
                                <Typography
                                    variant="body2"
                                    sx={{
                                        color: "black",
                                        opacity: 0.8,
                                        marginLeft: "10px",
                                        marginRight: "10px",
                                    }}
                                >
                                    <MarkdownRender mdText={card.content} />
                                </Typography>

                                {/* 分隔線 */}
                                <Divider
                                    sx={{
                                        backgroundColor: "rgba(255, 255, 255, 0.3)",
                                        width: "90%",
                                        alignSelf: "center",
                                    }}
                                />

                                {/* 作者資訊 */}
                                <Box sx={{ display: "flex", alignItems: "center", gap: 1, paddingTop: "10px", paddingLeft: "10px" }}>
                                    <Avatar sx={{ bgcolor: deepPurple[500] }} aria-label="author">
                                        {card.author ? card.author.charAt(0) : "M"}
                                    </Avatar>
                                    <Box>
                                        <Typography
                                            sx={{ fontSize: "12px", fontWeight: "bold", color: "black" }}
                                        >
                                            {card.author || "Michael"}
                                        </Typography>
                                        <Typography
                                            sx={{ fontSize: "10px", color: "black", opacity: 0.7 }}
                                        >
                                            {card.date || "Latest"}
                                        </Typography>
                                    </Box>
                                </Box>
                            </Box>
                        </Card>
                    </Link>
                </SwiperSlide>
            ))}
        </Swiper>
    );
};

export default HorizontalSwiperCards;

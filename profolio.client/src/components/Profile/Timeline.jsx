import { useState, useEffect, useRef, useCallback } from "react";
import {
    TimelineConnector,
    TimelineContent,
    TimelineDot,
    TimelineItem,
    TimelineOppositeContent,
    TimelineSeparator,
    Timeline
} from "@mui/lab";
import { motion } from "framer-motion";
import { Typography } from "@mui/material";
import Collapse from '@mui/material/Collapse';
import WorkIcon from "@mui/icons-material/Work";
import EngineeringIcon from '@mui/icons-material/Engineering';
import ConstructionIcon from '@mui/icons-material/Construction';
import ComputerIcon from '@mui/icons-material/Computer';
import ElectricBoltIcon from '@mui/icons-material/ElectricBolt';

const iconMap = {
    engineering: EngineeringIcon,
    work: WorkIcon,
    construction: ConstructionIcon,
    computer: ComputerIcon,
    electricBolt: ElectricBoltIcon
};

const TimelineEvent = ({ time, title, description, iconType }) => {
    const [expanded, setExpanded] = useState(false);
    const ref = useRef(null); //存取Dom元素
    const handleExpandClick = () => {
        setExpanded(!expanded);
    };
    //使用 useCallback 來確保 observer 只建立一次
    //useCallback 的第二個參數是 空陣列 []，代表 這個函數永遠不會變更，除非元件重新掛載。
    const observerCallback = useCallback(([entry]) => {
        setExpanded(entry.isIntersecting); //進入時展開timeline
    }, []);
    useEffect(() => {
        //當元素進入或離開可視範圍，會執行entry這個callback function
        const observer = new IntersectionObserver(observerCallback, { threshold: 0.3 }); // 30% 可見時觸發
        if (ref.current) observer.observe(ref.current); //ref.current就是被監測的DOM元素

        return () => observer.disconnect();
    }, [observerCallback]);

    const IconComponent = iconMap[iconType] || WorkIcon;

    return (
        <TimelineItem ref={ref}> {/*ref.current 就會指向這個 TimelineItem 的 DOM 元素。*/}
            <TimelineOppositeContent sx={{ m: "auto 0" }} align="right" variant="body2">
                {time}
            </TimelineOppositeContent>
            <TimelineSeparator>
                <TimelineConnector />
                <TimelineDot>
                    <IconComponent />
                </TimelineDot>
                <TimelineConnector />
            </TimelineSeparator>
            <TimelineContent>
                {/* 點擊活動文字展開內容 */}
                <motion.div
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.5, delay: 0.2 }}
                >
                    <Typography onClick={handleExpandClick} variant="h5" sx={{ cursor: "pointer" }}>
                        {title}
                    </Typography>
                </motion.div>
                {/*in屬性會決定是否展開，與expanded狀態綁定*/}
                {/*unmountOnExit 屬性：當內容收起時，從 DOM 中移除內容。*/}
                <Collapse in={expanded} timeout={600} unmountOnExit>
                    <motion.div
                        initial={{ opacity: 0, y: 10 }}
                        animate={{ opacity: expanded ? 1 : 0, y: expanded ? 0 : 10 }}
                        transition={{ duration: 0.3 }}
                    >
                        <Typography color="inherit" sx={{ mt: 2 }}>
                            {description}
                        </Typography>
                    </motion.div>
                </Collapse>
            </TimelineContent>
        </TimelineItem>
    );
};

const TimelineView = ({ events }) => {
    return (
        <Timeline position="alternate">
            {events && events.map((event, index) => (
                <TimelineEvent
                    key={index}
                    time={event.timePoint}
                    title={event.title}
                    description={event.description}
                    iconType={event.imageUrl} />
            ))}
        </Timeline>
    );
};
export default TimelineView;
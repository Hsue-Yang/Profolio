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
    const ref = useRef(null); //�s��Dom����
    const handleExpandClick = () => {
        setExpanded(!expanded);
    };
    //�ϥ� useCallback �ӽT�O observer �u�إߤ@��
    //useCallback ���ĤG�ӰѼƬO �Ű}�C []�A�N�� �o�Ө�ƥû����|�ܧ�A���D���󭫷s�����C
    const observerCallback = useCallback(([entry]) => {
        setExpanded(entry.isIntersecting); //�i�J�ɮi�}timeline
    }, []);
    useEffect(() => {
        //�����i�J�����}�i���d��A�|����entry�o��callback function
        const observer = new IntersectionObserver(observerCallback, { threshold: 0.3 }); // 30% �i����Ĳ�o
        if (ref.current) observer.observe(ref.current); //ref.current�N�O�Q�ʴ���DOM����

        return () => observer.disconnect();
    }, [observerCallback]);

    const IconComponent = iconMap[iconType] || WorkIcon;

    return (
        <TimelineItem ref={ref}> {/*ref.current �N�|���V�o�� TimelineItem �� DOM �����C*/}
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
                {/* �I�����ʤ�r�i�}���e */}
                <motion.div
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.5, delay: 0.2 }}
                >
                    <Typography onClick={handleExpandClick} variant="h5" sx={{ cursor: "pointer" }}>
                        {title}
                    </Typography>
                </motion.div>
                {/*in�ݩʷ|�M�w�O�_�i�}�A�Pexpanded���A�j�w*/}
                {/*unmountOnExit �ݩʡG���e���_�ɡA�q DOM ���������e�C*/}
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
import { Feedback } from '../../models/feedback';
import { AiFillStar } from 'react-icons/ai';
import dayjs from 'dayjs';
import realtiveTime from 'dayjs/plugin/relativeTime';

interface Props {
  feedback: Feedback;
}

dayjs.extend(realtiveTime);

export default function FeedbackCard({ feedback }: Props) {
  return (
    <li
      className={`feedback-card ${
        feedback.rating > 3
          ? 'positive'
          : feedback.rating === 3
          ? 'medium'
          : 'negative'
      }`}
      key={feedback.id}
    >
      <div className="star">
        {Array.from({ length: feedback.rating }).map((_, i) => {
          return <AiFillStar key={i} size={35} />;
        })}
      </div>
      <h3 className="h4">{feedback.title}</h3>
      <p>{feedback.description}</p>
      <div className="time">{dayjs(feedback.dateReviewed).fromNow()}</div>
    </li>
  );
}

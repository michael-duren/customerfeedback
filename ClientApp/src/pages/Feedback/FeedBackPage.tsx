import axios from 'axios';
import { useEffect, useState } from 'react';
import { Feedback } from '../../models/feedback';
import './FeedbackPage.styles.css';
import { AiFillStar } from 'react-icons/ai';
import dayjs from 'dayjs';
import realtiveTime from 'dayjs/plugin/relativeTime';

dayjs.extend(realtiveTime);

export default function FeedbackPage() {
  const [feedback, setFeedback] = useState<Feedback[]>([]);

  const getFeedback = async () => {
    const feedback = axios.get('/api/feedback');
    return (await feedback).data;
  };

  useEffect(() => {
    getFeedback().then((data) => setFeedback(data));
  }, []);

  console.log(feedback);

  return (
    <main className="feedback-page">
      <h1 className="h2">Feedback</h1>
      <div>
        <ul className="feedback-list">
          {feedback.map((feedback) => {
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
                <div className="time">
                  {dayjs(feedback.dateReviewed).fromNow()}
                </div>
              </li>
            );
          })}
        </ul>
      </div>
    </main>
  );
}

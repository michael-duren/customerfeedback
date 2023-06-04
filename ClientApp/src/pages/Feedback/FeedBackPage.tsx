import axios from 'axios';
import { useEffect, useState } from 'react';
import { Feedback } from '../../models/feedback';
import './FeedbackPage.styles.css';

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
              <li className="feedback-card" key={feedback.id}>
                <h3 className="h4">{feedback.title}</h3>
                <p>{feedback.description}</p>
              </li>
            );
          })}
        </ul>
      </div>
    </main>
  );
}

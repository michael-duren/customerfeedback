import './FeedbackPage.styles.css';
import { AiFillStar } from 'react-icons/ai';
import dayjs from 'dayjs';
import realtiveTime from 'dayjs/plugin/relativeTime';
import { Spinner } from 'reactstrap';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../stores/store';
import { useEffect } from 'react';

dayjs.extend(realtiveTime);

export default observer(function FeedbackPage() {
  const { feedbackStore } = useStore();
  const { isLoading, feedback } = feedbackStore;

  useEffect(() => {
    feedbackStore.loadFeedback();
  }, [feedbackStore]);

  return (
    <main className="feedback-page">
      <h1 className="h2">Feedback</h1>
      <div>
        {isLoading ? (
          <Spinner
            className="m-5"
            color="primary"
            style={{ height: '3rem', width: '3rem' }}
          >
            Loading Reviews...
          </Spinner>
        ) : (
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
        )}
      </div>
    </main>
  );
});

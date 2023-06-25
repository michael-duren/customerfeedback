import './FeedbackPage.styles.css';
import { Button, Spinner } from 'reactstrap';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../stores/store';
import { Fragment, useEffect, useState } from 'react';
import FeedbackForm from '../../components/Forms/FeedbackForm';
import FeedbackCard from '../../components/Cards/FeedbackCard';

export default observer(function FeedbackPage() {
  const { feedbackStore, userStore } = useStore();
  const { isLoading, feedback, loadFeedback } = feedbackStore;
  const { user } = userStore;
  const [isModalOpen, setIsModalOpen] = useState(false);
  const toggle = () => setIsModalOpen(!isModalOpen);

  useEffect(() => {
    loadFeedback();
  }, [feedbackStore]);

  return (
    <main className="feedback-page">
      <div
        className={`d-flex w-100 ${
          user ? 'justify-content-between' : 'justify-content-center'
        }`}
      >
        <h1 className="h2">Feedback</h1>
        {user && !(user.roles.includes("Admin")) && (
          <Button onClick={toggle} color="info">
            Add Feedback
          </Button>
        )}
      </div>
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
                <Fragment key={feedback.id}>
                  <FeedbackCard feedback={feedback} />
                </Fragment>
              );
            })}
          </ul>
        )}
      </div>
      <FeedbackForm
        modal={isModalOpen}
        setLoginModal={setIsModalOpen}
        toggle={toggle}
      />
    </main>
  );
});

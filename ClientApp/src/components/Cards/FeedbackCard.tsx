import { Feedback } from '../../models/feedback';
import { AiFillStar } from 'react-icons/ai';
import dayjs from 'dayjs';
import realtiveTime from 'dayjs/plugin/relativeTime';
import { BiTrash } from 'react-icons/bi';
import { Button, Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import { useState } from 'react';
import { useStore } from '../../stores/store';

interface Props {
  feedback: Feedback;
}

dayjs.extend(realtiveTime);

export default function FeedbackCard({ feedback }: Props) {
  const { feedbackStore, userStore } = useStore();
  const { user } = userStore;
  const { deleteFeedback } = feedbackStore;
  const [modal, setModal] = useState(false);
  const toggle = () => setModal(!modal);
  const deleteFeedbackHandler = () => {
    deleteFeedback(feedback.id);
    toggle();
  };

  return (
    <>
      <div className="feedback-container">
        {user && user.roles.includes("Admin") && (
          <button onClick={toggle} className="btn btn-outline-danger">
            <BiTrash />
          </button>
        )}
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
          <div className="reviewer">Reviewed by {feedback.userName}</div>
          <div className="time">{dayjs(feedback.dateReviewed).fromNow()}</div>
        </li>
      </div>
      {/* Modal */}
      <Modal isOpen={modal} toggle={toggle}>
        <ModalHeader toggle={toggle}>WARNING</ModalHeader>
        <ModalBody>
          Are you sure you want to delete the feedback item {feedback.title}?
        </ModalBody>
        <ModalFooter>
          <Button color="danger" onClick={deleteFeedbackHandler}>
            Delete
          </Button>{' '}
          <Button color="secondary" onClick={toggle}>
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </>
  );
}

import { makeAutoObservable } from 'mobx';
import { Feedback, FeedbackFormValues } from '../models/feedback';
import agent from '../api/agent';
import { toast } from 'react-toastify';

export default class FeedbackStore {
  feedback: Feedback[] = [];
  isLoading = false;

  constructor() {
    makeAutoObservable(this);
  }

  loadFeedback = async () => {
    this.setLoading(true);
    try {
      const feedback = await agent.FeedbackApi.getAll();
      this.setFeedback(feedback);
      this.setLoading(false);
    } catch (error) {
      console.log(error);
      this.setLoading(false);
    }
  };

  createFeedback = async (newFeedback: FeedbackFormValues) => {
    this.setLoading(true);
    try {
      await agent.FeedbackApi.create(newFeedback);
      toast.success('Feedback created successfully');
      this.loadFeedback();
      this.setLoading(false);
    } catch (error) {
      console.log(error);
      toast.error('Problem submitting feedback');
      this.setLoading(false);
    }
  };

  // setters
  setFeedback = (feedback: Feedback[]) => {
    this.feedback = feedback;
  };
  setLoading = (state: boolean) => {
    this.isLoading = state;
  };
}

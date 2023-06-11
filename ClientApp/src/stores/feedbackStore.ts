import { makeAutoObservable } from 'mobx';
import { Feedback } from '../models/feedback';
import agent from '../api/agent';

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

  // setters
  setFeedback = (feedback: Feedback[]) => {
    this.feedback = feedback;
  };
  setLoading = (state: boolean) => {
    this.isLoading = state;
  };
}

import {makeAutoObservable} from 'mobx';
import {Feedback, FeedbackFormValues} from '../models/feedback';
import agent from '../api/agent';
import {toast} from 'react-toastify';

export default class FeedbackStore {
    feedback: Feedback[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadFeedback = async (): Promise<void> => {
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

    createFeedback = async (newFeedback: FeedbackFormValues): Promise<void> => {
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

    deleteFeedback = async (id: string): Promise<void> => {
        this.setLoading(true);
        try {
            await agent.FeedbackApi.delete(id);
            toast.success('Feedback deleted successfully');
            this.loadFeedback();
            this.setLoading(false);
        } catch (error) {
            console.log(error);
            toast.error('Problem deleting feedback');
            this.setLoading(false);
        }
    };

    // setters
    setFeedback = (feedback: Feedback[]): void => {
        this.feedback = feedback;
    };
    setLoading = (state: boolean): void => {
        this.isLoading = state;
    };
}

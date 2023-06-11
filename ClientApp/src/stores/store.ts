import { createContext, useContext } from 'react';
import FeedbackStore from './feedbackStore';

interface Store {
  feedbackStore: FeedbackStore;
}

export const store: Store = {
  feedbackStore: new FeedbackStore(),
};

export const StoreContext = createContext(store);

export const useStore = () => {
  return useContext(StoreContext);
};

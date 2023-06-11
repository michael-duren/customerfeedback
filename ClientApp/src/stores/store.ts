import { createContext, useContext } from 'react';
import FeedbackStore from './feedbackStore';
import UserStore from './userStore';

interface Store {
  feedbackStore: FeedbackStore;
  userStore: UserStore;
}

export const store: Store = {
  feedbackStore: new FeedbackStore(),
  userStore: new UserStore(),
};

export const StoreContext = createContext(store);

export const useStore = () => {
  return useContext(StoreContext);
};

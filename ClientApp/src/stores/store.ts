import { createContext, useContext } from 'react';
import FeedbackStore from './feedbackStore';
import UserStore from './userStore';
import CommonStore from './commonStore';

interface Store {
  feedbackStore: FeedbackStore;
  userStore: UserStore;
  commonStore: CommonStore;
}

export const store: Store = {
  feedbackStore: new FeedbackStore(),
  userStore: new UserStore(),
  commonStore: new CommonStore(),
};

export const StoreContext = createContext(store);

export const useStore = () => {
  return useContext(StoreContext);
};

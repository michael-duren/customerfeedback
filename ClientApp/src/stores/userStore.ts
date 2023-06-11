import { makeAutoObservable, runInAction } from 'mobx';
import { User, UserFormValues } from '../models/user';
import agent from '../api/agent';
import { store } from './store';
import { router } from '../routes/router';

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    return !!this.user;
  }

  login = async (creds: UserFormValues) => {
    try {
      const user = await agent.AccountApi.login(creds);
      store.commonStore.setToken(user.token);
      runInAction(() => (this.user = user));
      router.navigate('/feedback');
    } catch (error) {
      throw error;
    }
  };

  logout = () => {
    store.commonStore.setToken(null);
    this.user = null;
    router.navigate('/');
  };

  // setters
  setUser = (user: User) => {
    this.user = user;
  };

  // getters
  getUser = async () => {
    try {
      const user = await agent.AccountApi.getCurrentUser();
      runInAction(() => (this.user = user));
    } catch (error) {
      console.log(error);
    }
  };
}

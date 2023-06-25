import {makeAutoObservable, runInAction} from 'mobx';
import {User, UserFormValues} from '../models/user';
import agent from '../api/agent';
import {store} from './store';
import {router} from '../pages/router';

export default class UserStore {
  user: User | null = null;
  loadingUser: boolean = true;

  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    return !!this.user;
  }

  login = async (creds: UserFormValues): Promise<void> => {
    try {
      const user = await agent.AccountApi.login(creds);
      store.commonStore.setToken(user.token);
      runInAction(() => (this.user = user));
      console.log(user);
      await router.navigate('/feedback');
    } catch (error) {
      throw error;
    }
  };

  register = async (creds: UserFormValues): Promise<void> => {
    try {
      const user = await agent.AccountApi.register(creds);
      store.commonStore.setToken(user.token);
      runInAction(() => (this.user = user));
      await router.navigate('/feedback');
    } catch (error) {
      throw error;
    }
  };

  logout = async (): Promise<void> => {
    store.commonStore.setToken(null);
    this.user = null;
    await router.navigate('/');
  };

  // setters
  setUser = (user: User) => {
    this.user = user;
  };

  setLoadingUser = (loading: boolean) => {
    this.loadingUser = loading;
  };

  // getters
  getUser = async (): Promise<void> => {
    try {
      const user = await agent.AccountApi.getCurrentUser();
      runInAction(() => {
        this.user = user
        console.log(user)
      });
    } catch (error) {
      console.log(error);
    }
  };
}

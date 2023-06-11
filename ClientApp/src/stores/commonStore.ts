import { makeAutoObservable, reaction } from 'mobx';
import { IServerError } from '../models/serverError';

export default class CommonStore {
  error: IServerError | null = null;
  token: string | null = localStorage.getItem('jwt');
  appLoaded: boolean = false;

  constructor() {
    makeAutoObservable(this);

    // reacts to changes in the token property
    reaction(
      () => this.token,
      (token) => {
        if (token) {
          localStorage.setItem('jwt', token);
        } else {
          localStorage.removeItem('jwt');
        }
      }
    );
  }

  setServerError = (error: IServerError) => {
    this.error = error;
  };

  setToken = (token: string | null) => {
    this.token = token;
  };

  setAppLoaded = () => {
    this.appLoaded = true;
  };
}

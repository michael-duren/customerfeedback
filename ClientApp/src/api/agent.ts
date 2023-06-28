import axios, {AxiosError, AxiosResponse} from 'axios';
import {Feedback, FeedbackFormValues} from '../models/feedback';
import {User, UserFormValues} from '../models/user';
import {toast} from 'react-toastify';
import {router} from '../pages/router';
import {store} from '../stores/store';

axios.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const {data, status, config} = error.response as AxiosResponse;
    switch (status) {
      case 400:
        if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
          await router.navigate('/not-found');
        }
        if (data.errors) {
          const modalStateErrors = [];
          for (const key in data.errors) {
            if (data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }

          throw modalStateErrors.flat();
        } else {
          toast.error(data);
        }
        break;
      case 401:
        toast.error('unauthorized');
        break;
      case 403:
        toast.error('forbidden');
        break;
      case 404:
        await router.navigate('/not-found');
        break;
      case 500:
        store.commonStore.setServerError(data);
        await router.navigate('/server-error');
        break;
    }

    return Promise.reject(error);
  }
);

const responseData = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use((config) => {
  const token = store.commonStore.token;
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseData),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseData),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseData),
  del: <T>(url: string) => axios.delete<T>(url).then(responseData),
};

const FeedbackApi = {
  getAll: () => requests.get<Feedback[]>('/api/feedback/'),
  create: (newFeedback: FeedbackFormValues) =>
    requests.post<void>('/api/feedback/', newFeedback),
  update: (id: string, updatedFeedback: Feedback) =>
    requests.put<void>(`/api/feedback/${id}`, updatedFeedback),
  delete: (id: string) => requests.del<void>(`/api/feedback/${id}`),
};

const AccountApi = {
  getCurrentUser: () => requests.get<User>('/api/account'),
  login: (user: UserFormValues) =>
    requests.post<User>('/api/account/login', user),
  register: (user: UserFormValues) =>
    requests.post<User>('/api/account/register', user),
  getAllUsers: () => requests.get<User[]>('/api/admin/users'),
};

const agent = {
  FeedbackApi,
  AccountApi,
};

export default agent;

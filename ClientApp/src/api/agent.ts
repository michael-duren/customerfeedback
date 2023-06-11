import axios, { AxiosResponse } from 'axios';
import { Feedback } from '../models/feedback';

const responseData = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseData),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseData),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseData),
  del: <T>(url: string) => axios.delete<T>(url).then(responseData),
};

const FeedbackApi = {
  getAll: () => requests.get<Feedback[]>('/api/feedback/'),
  create: (newFeedback: Feedback) =>
    requests.post<void>('/api/feedback/', newFeedback),
  update: (id: number, updatedFeedback: Feedback) =>
    requests.put<void>(`/api/feedback/${id}`, updatedFeedback),
  delete: (id: number) => requests.del<void>(`/api/feedback/${id}`),
};

const agent = {
  FeedbackApi,
};

export default agent;

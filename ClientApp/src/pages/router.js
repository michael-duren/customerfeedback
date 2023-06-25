import App from '../App';
import { createBrowserRouter } from 'react-router-dom';
import HomePage from './Home/HomePage';
import FeedBackPage from './Feedback/FeedBackPage';
import NotFound from './Errors/NotFound';
import ServerError from './Errors/ServerError';
import { Navigate } from 'react-router-dom';
import UserList from "./Admin/UserList/UserList";

export const routes = [
  {
    path: '/',
    element: <App />,
    children: [
      { path: '', element: <HomePage /> },
      { path: '/feedback', element: <FeedBackPage /> },
      { path: '/admin/user-list', element: <UserList />},
      { path: 'not-found', element: <NotFound /> },
      { path: 'server-error', element: <ServerError /> },
      { path: '*', element: <Navigate replace to={'/not-found'} /> },
    ],
  },
];

export const router = createBrowserRouter(routes);

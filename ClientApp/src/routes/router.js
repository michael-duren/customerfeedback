import App from '../App';
import { createBrowserRouter } from 'react-router-dom';
import HomePage from '../pages/Home/HomePage';
import FeedBackPage from '../pages/Feedback/FeedBackPage';
import NotFound from '../pages/Errors/NotFound';
import ServerError from '../pages/Errors/ServerError';
import { Navigate } from 'react-router-dom';

export const routes = [
  {
    path: '/',
    element: <App />,
    children: [
      { path: '', element: <HomePage /> },
      { path: '/feedback', element: <FeedBackPage /> },
      { path: 'not-found', element: <NotFound /> },
      { path: 'server-error', element: <ServerError /> },
      { path: '*', element: <Navigate replace to={'/not-found'} /> },
    ],
  },
];

export const router = createBrowserRouter(routes);

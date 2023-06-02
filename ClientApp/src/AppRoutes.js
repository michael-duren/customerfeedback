import HomePage from './pages/HomePage.tsx';
import FeedbackPage from './pages/FeedBackPage.tsx';

const AppRoutes = [
  {
    index: true,
    element: <HomePage />,
  },
  {
    path: '/feedback',
    element: <FeedbackPage />,
  },
];

export default AppRoutes;
